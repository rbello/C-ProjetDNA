using NetworkComputeFramework.Data;
using NetworkComputeFramework.Node;

namespace NetworkComputeFramework.Worker
{
    public interface IWorker
    {

        INode Node { get; }

        int ID { get; }

        bool Available { get; set; }

        object Execute<T>(DataChunk<T> chunk, DataProcess<T> process);

    }
}
