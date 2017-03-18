using NetworkComputeFramework.Data;
using System.Collections.Generic;

namespace NetworkComputeFramework.MapReduce
{
    public interface IMapper<T> : IEnumerable<DataChunk<T>>
    {
        long DataLength { get; }
        int ChunkLength { get; }
        int ChunkRemains { get; }
        int ChunkCount { get; }
    }
}
