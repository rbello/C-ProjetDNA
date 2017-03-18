using NetworkComputeFramework.Data;
using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.RunMode
{
    public class ServerMode<S, T> : AbstractRunMode
    {

        protected IFactory<S, T> factory;

        public ServerMode(IFactory<S, T> factory) : base()
        {
            this.factory = factory;
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
            node.Start();
        }

        protected override void Start(params object[] args)
        {
            // Open data source
            IDataLoader<S, T> loader = factory.CreateDataLoader();
            IDataReader<T> reader = loader.Open((S) args[0]);
            // Create the job
            Job<T> job = factory.CreateJob((string)args[1], reader);
            // Run the job into the workers' pool
            WorkerPool.Process(job);
        }
    }
}
