using System;
using NetworkComputeFramework.Node;
using System.Threading;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;

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

        public void Execute<T>(DataChunk<T> chunk, IReducer<T> reducer)
        {
            Thread.Sleep(5000);
            reducer.Reduce(chunk);
        }

        public override string ToString()
        {
            return "Worker " + ID;
        }

    }
}
