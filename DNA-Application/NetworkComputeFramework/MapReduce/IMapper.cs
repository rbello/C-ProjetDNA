using System;
using System.Collections.Generic;

namespace NetworkComputeFramework.MapReduce
{
    public interface IMapper<T> : IEnumerable<T[]>
    {
        long DataLength { get; }
        int ChunkLength { get; }
        int ChunkRemains { get; }
        int ChunkCount { get; }
    }
}
