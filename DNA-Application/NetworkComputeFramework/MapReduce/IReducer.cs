using NetworkComputeFramework.Data;

namespace NetworkComputeFramework.MapReduce
{
    public interface IReducer<T>
    {
        void Reduce(DataChunk<T> chunk);
    }
}
