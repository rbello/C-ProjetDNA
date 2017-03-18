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

        public int WorkersCount { get; protected set; }

        public WorkerPool()
        {
        }

        public void AddNode(INode node)
        {
            if (nodes.Contains(node))
                throw new ArgumentException("Node allready exists in worker pool");
            nodes.Add(node);
            WorkersCount += node.Workers.Length;
            OnNodeConnected?.Invoke(node);
        }

        public void Process<T>(Job<T> job)
        {
            OnWorkerPoolMessage?.Invoke("Data length: " + job.DataReader.Length + " records", 1);
            // Create mapper
            IMapper <T> mapper = job.CreateMapper(this);
            // Map data
            mapper.Map(delegate (int number, long from, long to, long length, T[] data)
            {
                OnWorkerPoolMessage?.Invoke(
                    string.Format("Chunk {0} ({1}-{2}) Length={3}", number, from, to, length)
                    , 1);
            });
        }
    }
}
