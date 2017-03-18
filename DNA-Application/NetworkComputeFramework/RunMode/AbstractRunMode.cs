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
        public Exception LastError { get; set; }

        public event RunModeStateHandler OnStateChanged;

        public WorkerPool WorkerPool { get; private set; }
 
        public AbstractRunMode()
        {
            WorkerPool = new WorkerPool();
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

        public void Start(Action success, Action<Exception> failure, params object[] args)
        {
            new Thread(delegate ()
            {
                try
                {
                    Start(args);
                    success();
                }
                catch (Exception ex)
                {
                    failure(ex);
                }

            }).Start();
        }

        protected abstract void Start(params object[] args);

    }
}
