using NetworkComputeFramework.Worker;
using System;

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
    /// A run mode represents a way of running the application. Typically, server and client modes.
    /// This class implements the Strategy design pattern.
    /// </summary>
    public interface IRunMode
    {

        /// <summary>
        /// Prepare run mode. This method can be asynchronous.
        /// </summary>
        /// <param name="success">Callback when init has succeeded</param>
        /// <param name="failure">Callback when init encounter an exception</param>
        /// <param name="args">Arguments parameters</param>
        void Init(Action success, Action<Exception> failure, params object[] args);

        /// <summary>
        /// Start run mode. This method can be asynchronous.
        /// </summary>
        /// <param name="success">Callback when init has succeeded</param>
        /// <param name="failure">Callback when init encounter an exception</param>
        /// <param name="args">Arguments parameters</param>
        void Start(Action<object> success, Action<Exception> failure, params object[] args);

        /// <summary>
        /// Event triggered when the state changed.
        /// </summary>
        event Action<RunState> OnStateChanged;

        /// <summary>
        /// Get the worker pool.
        /// </summary>
        WorkerPool WorkerPool { get; }

        /// <summary>
        /// Stop the current run mode.
        /// </summary>
        void Stop();

        /// <summary>
        /// Get the last exception thrown during execution.
        /// </summary>
        [Obsolete]
        Exception LastException { get; }

    }
}
