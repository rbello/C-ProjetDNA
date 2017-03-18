using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using NetworkComputeFramework.Node;
using System;
using System.Collections.Generic;

namespace NetworkComputeFramework.Worker
{
    public class WorkerPool
    {

        public event Action<INode> OnNodeConnected;
        public event Action<INode> OnNodeDisconnected;
        public event Action<string, int> OnWorkerPoolMessage;

        IList<INode> nodes = new List<INode>();

        private int totalWorkersCount = 0;
        private Func<IMapper> mapperFactory;

        public WorkerPool(Func<IMapper> mapperFactory)
        {
            this.mapperFactory = mapperFactory;
        }

        public void AddNode(INode node)
        {
            if (nodes.Contains(node))
                throw new ArgumentException("Node allready exists in worker pool");
            nodes.Add(node);
            totalWorkersCount += node.Workers.Length;
            OnNodeConnected?.Invoke(node);
        }

        public void Process<T>(Job<T> job)
        {
            // Create mapper
            IMapper m = mapperFactory.Invoke();

            long chunkSize = (long) Math.Floor((double)(job.DataReader.Length / totalWorkersCount));
            OnWorkerPoolMessage?.Invoke("Data length: " + job.DataReader.Length, 1);
            OnWorkerPoolMessage?.Invoke("Available workers: " + totalWorkersCount, 1);
            OnWorkerPoolMessage?.Invoke("\ton " + nodes.Count + " node(s)", 1);
            OnWorkerPoolMessage?.Invoke("Chunk length: " + chunkSize + " records per worker", 1);
        }
    }
}
