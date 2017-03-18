using System;
using NetworkComputeFramework.Node;
using System.Threading;

namespace NetworkComputeFramework.Worker
{
    public class ThreadWorker : IWorker
    {

        public ThreadWorker(LocalNode node, int id)
        {
            Node = node;
            ID = id;
            Available = true;
        }

        public INode Node { get; protected set; }

        public int ID { get; protected set; }

        public bool Available { get; set; }

        public object Execute<T>(T[] t)
        {
            Thread.Sleep(5000);
            return null;
        }

        public override string ToString()
        {
            return "Worker " + ID;
        }

    }
}
