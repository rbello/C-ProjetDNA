using NetworkComputeFramework.Worker;
using System;

namespace NetworkComputeFramework.Node
{
    public interface INode
    {
        IWorker[] Workers { get; }
    }
}
