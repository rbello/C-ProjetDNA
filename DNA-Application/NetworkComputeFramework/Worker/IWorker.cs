using NetworkComputeFramework.Data;
using NetworkComputeFramework.Node;

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
    /// Abstraction for a data process worker.
    /// </summary>
    public interface IWorker
    {

        /// <summary>
        /// Get the parent node of this worker.
        /// </summary>
        INode Node { get; }

        /// <summary>
        /// Get the worker identifier on his node.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Returns true if this worker is available for working.
        /// </summary>
        bool Available { get; set; }

        /// <summary>
        /// Process a chunk of data.
        /// This method can throw exceptions.
        /// </summary>
        /// <typeparam name="T">Type of the unitary data processed</typeparam>
        /// <param name="chunk">The chunk of data to process</param>
        /// <param name="process">The process to execute on data</param>
        /// <returns>The result of the process</returns>
        object Execute<T>(DataChunk<T> chunk, DataProcess<T> process);

    }
}
