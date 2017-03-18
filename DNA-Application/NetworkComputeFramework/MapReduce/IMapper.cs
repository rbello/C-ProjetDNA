using System;
using NetworkComputeFramework.Data;

namespace NetworkComputeFramework.MapReduce
{
    public interface IMapper<T>
    {
        void Map(Action<int, long, long, long, T[]> chunkIterator);
    }
}
