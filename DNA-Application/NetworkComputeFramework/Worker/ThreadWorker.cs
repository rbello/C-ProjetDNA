using NetworkComputeFramework.Node;
using NetworkComputeFramework.Data;

/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.Worker
{

    /// <summary>
    /// A simple worker intended to execute process on data chunk synchronously.
    /// </summary>
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

        public object Execute<T>(DataChunk<T> chunk, DataProcess<T> process)
        {
            return process.CreateReducer().Reduce(chunk);
        }

        public override string ToString()
        {
            return "Worker " + ID;
        }

    }
}
