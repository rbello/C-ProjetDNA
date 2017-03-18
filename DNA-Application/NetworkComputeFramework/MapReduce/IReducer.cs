using NetworkComputeFramework.Data;

namespace NetworkComputeFramework.MapReduce
{
    public interface IReducer<T>
    {
        object Reduce(DataChunk<T> chunk);
    }
}
