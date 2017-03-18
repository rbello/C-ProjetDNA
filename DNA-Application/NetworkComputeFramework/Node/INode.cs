using NetworkComputeFramework.Worker;
using System;
using System.Collections.Generic;

namespace NetworkComputeFramework.Node
{
    public interface INode
    {
        IList<IWorker> Workers { get; }
        string Address { get; }
        float CpuUsage { get; }
        float MemoryUsage { get; }
        int ActiveWorkersCount { get; }
        bool Active { get; }
    }
}
