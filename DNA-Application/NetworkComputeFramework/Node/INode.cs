using NetworkComputeFramework.Worker;
using System.Collections.Generic;

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
    /// Interface for executing nodes. A node is either a local node composed with running threads,
    /// or a remote node connected by sockets.
    /// </summary>
    public interface INode
    {

        /// <summary>
        /// Get the list of available workers (threads).
        /// </summary>
        IList<IWorker> Workers { get; }

        /// <summary>
        /// Get simple representation of network address of this node.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Get current CPU usage approximation in percent of this node.
        /// </summary>
        float CpuUsage { get; }

        /// <summary>
        /// Get current memory usage approximation in MB of this node.
        /// </summary>
        float MemoryUsage { get; }

        /// <summary>
        /// Returns the number of workers currently processing data.
        /// </summary>
        int ActiveWorkersCount { get; }

        /// <summary>
        /// Returns true if a least one worker of this node is processing data.
        /// </summary>
        bool Active { get; }

    }
}
