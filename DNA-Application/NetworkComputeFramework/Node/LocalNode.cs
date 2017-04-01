using NetworkComputeFramework.Worker;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
    /// A local node, running on this computer.
    /// </summary>
    public class LocalNode : INode
    {

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public LocalNode(int localThreadsCount)
        {
            Workers = new ThreadWorker[localThreadsCount];
            for (int i = 0; i < localThreadsCount; ++i)
            {
                Workers[i] = new ThreadWorker(this, i);
            }
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public IList<IWorker> Workers { get; protected set; }

        public string Address => "127.0.0.1";

        public float CpuUsage => cpuCounter.NextValue();

        public float MemoryUsage => ramCounter.NextValue();

        public int ActiveWorkersCount => Workers.Where(worker => !worker.Available).Count();

        public bool Active => (ActiveWorkersCount > 0);

        public override string ToString() => "LocalNode [127.0.0.1]";

    }
}
