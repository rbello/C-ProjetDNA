using System;
using NetworkComputeFramework.Node;

namespace NetworkComputeFramework.Worker
{
    public class ThreadWorker : IWorker
    {

        public ThreadWorker(LocalNode node, int id)
        {
            this.Node = node;
            this.ID = id;
        }

        public INode Node { get; protected set; }

        public int ID { get; protected set; }
    }
}
