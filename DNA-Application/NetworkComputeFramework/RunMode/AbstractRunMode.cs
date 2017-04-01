using NetworkComputeFramework.Worker;
using System;
using System.Threading;

/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.RunMode
{
    /// <summary>
    /// Abstract class for application run mode. Provides a standard to run the mode asynchronously,
    /// and catch exceptions.
    /// </summary>
    public abstract class AbstractRunMode : IRunMode
    {

        /// <summary>
        /// Event triggered when the state changed.
        /// </summary>
        public event Action<RunState> OnStateChanged;

        /// <summary>
        /// Get the worker pool.
        /// </summary>
        public WorkerPool WorkerPool { get; private set; }

        /// <summary>
        /// Current running state.
        /// </summary>
        public RunState State { get; private set; }

        /// <summary>
        /// Get the last exception thrown during execution.
        /// </summary>
        [Obsolete]
        public Exception LastException { get; protected set; }

        /// <summary>
        /// Get the logger delegate (used to route logs to form).
        /// </summary>
        public Action<string> Logger { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger delegate to use</param>
        public AbstractRunMode(Action<string> logger)
        {
            Logger = logger;
            WorkerPool = new WorkerPool(this.ChangeState);
        }

        /// <summary>
        /// Internal method to change current run state.
        /// </summary>
        /// <param name="newState">New state</param>
        protected void ChangeState(RunState newState)
        {
            if (State == newState) return;
            State = newState;
            OnStateChanged?.Invoke(newState);
        }

        /// <summary>
        /// Prepare run mode. This method is asynchronous.
        /// </summary>
        /// <param name="success">Callback when init has succeeded</param>
        /// <param name="failure">Callback when init encounter an exception</param>
        /// <param name="args">Arguments parameters</param>
        public void Init(Action success, Action<Exception> failure, params object[] args)
        {
            new Thread(delegate ()
            {
                try
                {
                    Init(args);
                    success();
                }
                catch (Exception ex)
                {
                    failure(ex);
                }

            }).Start();
        }

        /// <summary>
        /// Abstract method to implements with concrete initialization.
        /// </summary>
        /// <param name="args">Arguments parameters</param>
        protected abstract void Init(params object[] args);

        /// <summary>
        /// Start run mode. This method is asynchronous.
        /// </summary>
        /// <param name="success">Callback when init has succeeded</param>
        /// <param name="failure">Callback when init encounter an exception</param>
        /// <param name="args">Arguments parameters</param>
        public void Start(Action<object> success, Action<Exception> failure, params object[] args)
        {
            new Thread(delegate ()
            {
                try
                {
                    StartSynch(success, failure, args);
                }
                catch (Exception ex)
                {
                    failure(ex);
                }

            }).Start();
        }

        /// <summary>
        /// Abstract method to implements with concrete starting process.
        /// </summary>
        /// <param name="success">Callback when start has succeeded</param>
        /// <param name="failure">Callback when start encounter an exception</param>
        /// <param name="args">Arguments parameters</param>
        protected abstract void StartSynch(Action<object> success, Action<Exception> failure, params object[] args);

        /// <summary>
        /// Abstract method to implements with concrete stop procedure.
        /// </summary>
        public abstract void Stop();

    }
}
