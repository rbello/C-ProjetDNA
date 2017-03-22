using System;
using System.Collections.Generic;
using NetworkComputeFramework.Net;
using NetworkComputeFramework.Worker;

namespace NetworkComputeFramework.Node
{
    public class RemoteNode : INode
    {
        private ClientSocket clientSocket;

        public RemoteNode(ClientSocket clientSocket)
        {
            this.clientSocket = clientSocket;
            Workers = new List<IWorker>();
        }

        public IList<IWorker> Workers { get; protected set; }

        public string Address => clientSocket.RemoteSocket.RemoteEndPoint.ToString();

        public float CpuUsage => 0;

        public float MemoryUsage => 0;

        public int ActiveWorkersCount => 0;

        public bool Active => false;

        public override string ToString()
        {
            return "RemoteNode [" + Address + "]";
        }
    }
}
