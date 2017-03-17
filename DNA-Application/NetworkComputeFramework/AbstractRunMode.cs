using System;
using System.Threading;

namespace NetworkComputeFramework
{
    public abstract class AbstractRunMode : IRunMode
    {
        public Exception LastError { get; set; }

        public event RunModeStateHandler OnStateChanged;

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
