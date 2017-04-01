using System.Collections.Generic;
using NetworkComputeFramework.Net;
using NetworkComputeFramework.Worker;

/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.Node
{

    /// <summary>
    /// A node connected through the network.
    /// </summary>
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

        public override string ToString() => "RemoteNode [" + Address + "]";

    }
}
