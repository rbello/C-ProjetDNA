using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.Worker
{
    public interface IWorker
    {
        INode Node { get; }

        int ID { get; }

        bool Available { get; set; }

        object Execute<T>(T[] t);
    }
}
