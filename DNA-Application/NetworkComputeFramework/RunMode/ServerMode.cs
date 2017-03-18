using NetworkComputeFramework.Data;
using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.RunMode
{
    public class ServerMode<S, T> : AbstractRunMode
    {

        protected IDataApplication<S, T> application;

        public DataProcess<T> CurrentProcess { get; private set; }

        public ServerMode(IDataApplication<S, T> application) : base()
        {
            this.application = application;
        }

        protected override void Init(params object[] args)
        {
            CreateServerSocket(Convert.ToInt32(args[0]));
            RunLocalNode(Convert.ToInt32(args[1]));
            ChangeState(RunState.IDLE);
        }

        private void CreateServerSocket(int portNumber)
        {

        }

        private void RunLocalNode(int localThreadsCount)
        {
            var node = new LocalNode(localThreadsCount);
            WorkerPool.AddNode(node);
            node.Init();
        }

        protected override void StartSynch(Action<object> success, Action<Exception> failure, params object[] args)
        {
            // Change run mode state
            ChangeState(RunState.LOAD_BEGIN);
            // Open data source
            IDataLoader<S, T> loader = application.CreateDataLoader();
            IDataReader<T> reader = loader.Open((S) args[0]);
            // Create the process
            CurrentProcess = application.CreateProcess((string)args[1], reader);
            // Run the process into the workers' pool
            WorkerPool.Process(
                // Process to execute
                CurrentProcess,
                // On success
                delegate (object finalResult)
                {
                    CurrentProcess.CleanUp();
                    ChangeState(RunState.REDUCE_DONE);
                    success.Invoke(finalResult);
                },
                // On failure
                delegate (Exception ex)
                {
                    CurrentProcess.CleanUp();
                    ChangeState(RunState.FAILURE);
                    LastException = ex;
                    failure.Invoke(ex);
                });
        }

        public override void Stop()
        {
            if (CurrentProcess != null)
            {
                CurrentProcess.Interrupted = true;
            }
        }
    }
}
