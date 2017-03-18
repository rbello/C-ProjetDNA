using NetworkComputeFramework.Data;
using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.RunMode
{
    public class ServerMode<S, T> : AbstractRunMode
    {

        protected IDataApplication<S, T> application;

        public ServerMode(IDataApplication<S, T> factory) : base()
        {
            this.application = factory;
        }

        protected override void Init(params object[] args)
        {
            CreateServerSocket(Convert.ToInt32(args[0]));
            RunLocalNode(Convert.ToInt32(args[1]));
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

        protected override void Start(params object[] args)
        {
            // Change run mode state
            ChangeState(RunState.LOADING_DATA);
            // Open data source
            IDataLoader<S, T> loader = application.CreateDataLoader();
            IDataReader<T> reader = loader.Open((S) args[0]);
            // Create the job
            var job = application.CreateJob((string)args[1], reader);
            // Run the job into the workers' pool
            WorkerPool.Process(job);
        }

        
    }
}
