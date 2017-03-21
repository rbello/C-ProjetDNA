using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using NetworkComputeFramework.Node;
using NetworkComputeFramework.Worker;
using System;
using System.Threading;

namespace NetworkComputeFramework.RunMode
{
    public abstract class AbstractRunMode : IRunMode
    {

        public event Action<RunState> OnStateChanged;

        public WorkerPool WorkerPool { get; private set; }

        public RunState State { get; private set; }

        public Exception LastException { get; protected set; }

        public Action<string> Logger { get; private set; }

        public AbstractRunMode(Action<string> logger)
        {
            Logger = logger;
            WorkerPool = new WorkerPool(this.ChangeState);
        }

        protected void ChangeState(RunState newState)
        {
            if (State == newState) return;
            State = newState;
            OnStateChanged?.Invoke(newState);
        }

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

        protected abstract void Init(params object[] args);

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

        protected abstract void StartSynch(Action<object> success, Action<Exception> failure, params object[] args);

        public abstract void Stop();
    }
}
