using System;
using System.Collections.Generic;
using NetworkComputeFramework.Worker;

namespace NetworkComputeFramework.Node
{
    public class RemoteNode : INode
    {
        public IList<IWorker> Workers => throw new NotImplementedException();

        public string Address => throw new NotImplementedException();

        public float CpuUsage => throw new NotImplementedException();

        public float MemoryUsage => throw new NotImplementedException();

        public int ActiveWorkersCount => throw new NotImplementedException();

        public bool Active => throw new NotImplementedException();
    }
}
