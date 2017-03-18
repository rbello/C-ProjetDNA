using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.Worker
{
    public interface IWorker
    {
        INode Node { get; }

        int ID { get; }
    }
}
