using NetworkComputeFramework.Data;
using System;

namespace NetworkComputeFramework.MapReduce
{
    public interface IMapper<T> : IDisposable
    {
        long DataLength { get; }

        int ChunkPreferredLength { get; }

        int ChunkRemainsLength { get; }

        int ChunkCount { get; }

        bool Active { get; }

        DataChunk<T> NextChunk();

    }
}
