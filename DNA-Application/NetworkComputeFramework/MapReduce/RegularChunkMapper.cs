using System;
using System.Collections;
using System.Collections.Generic;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.Worker;

namespace NetworkComputeFramework.MapReduce
{
    public class RegularChunkMapper<T> : IMapper<T>
    {

        public RegularChunkMapper(int chunkLength, IDataReader<T> dataSource)
        {
            DataSource = dataSource;

            ChunkLength = chunkLength;
            ChunkCount = (int)(DataSource.Length / ChunkLength);
            ChunkRemains = (int)(DataLength - ChunkCount * ChunkLength);
            if (ChunkRemains > 0) ChunkCount++;
        }

        public IDataReader<T> DataSource { get; protected set; }

        public long DataLength => DataSource.Length;

        public int ChunkLength { get; private set; }

        public int ChunkRemains { get; private set; }

        public int ChunkCount { get; private set; }

        public IEnumerator<T[]> GetEnumerator()
        {
            for (int i = 0; i < ChunkCount; ++i)
            {
                yield return DataSource.Next(ChunkLength);
            }
            if (ChunkRemains > 0) yield return DataSource.Next(ChunkRemains);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
