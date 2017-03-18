using System;
using System.Collections.Generic;

namespace NetworkComputeFramework.Node
{
    public class WorkerPool
    {

        public event Action<Node> OnNodeConnected;
        public event Action<Node> OnNodeDisconnected;

        IList<Node> nodes = new List<Node>();

        public void AddNode(Node node)
        {
            if (nodes.Contains(node))
                throw new ArgumentException("Node allready exists in worker pool");
            nodes.Add(node);
            OnNodeConnected?.Invoke(node);
        }

        public void Process<T>(Job<T> job)
        {
            
        }
    }
}
