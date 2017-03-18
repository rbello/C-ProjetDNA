using System;
using System.Collections.Generic;
using NetworkComputeFramework.Data;

namespace NetworkComputeFramework.MapReduce
{
    public class RegularChunkMapper<T> : IMapper<T>
    {
        private DataChunk<T>[] chunks;

        public RegularChunkMapper(int chunkLength, IDataReader<T> dataSource)
        {
            DataSource = dataSource;
            ChunkLength = chunkLength;
            ChunkCount = (int)(DataSource.Length / ChunkLength);
            ChunkRemains = (int)(DataLength - ChunkCount * ChunkLength);
            if (ChunkRemains > 0) ChunkCount++;
            chunks = new DataChunk<T>[ChunkCount];
        }

        public IDataReader<T> DataSource { get; protected set; }

        public long DataLength => DataSource.Length;

        public int ChunkLength { get; private set; }

        public int ChunkRemains { get; private set; }

        public int ChunkCount { get; private set; }

        public bool Active
        {
            get
            {
                for (int i = 0; i < ChunkCount; ++i)
                {
                    // Not yet loaded
                    if (chunks[i] == null) return true;
                    // Not yet done
                    if (chunks[i].State != ChunkState.Done) return true;
                }
                // Each chunk is loaded and done
                return false;
            }
        }

        public void Dispose()
        {
            chunks = null;
            ChunkLength = ChunkRemains = ChunkCount = 0;
            DataSource = null;
        }

        [Obsolete]
        public IEnumerator<DataChunk<T>> GetEnumerator()
        {
            for (int i = 0; i < ChunkCount; ++i)
            {
                yield return new DataChunk<T>(DataSource.Next(ChunkLength), i);
            }
            if (ChunkRemains > 0) yield return new DataChunk<T>(DataSource.Next(ChunkRemains), ChunkCount);
        }

        public DataChunk<T> NextChunk()
        {
            for (int i = 0; i < ChunkCount; ++i)
            {
                // If the chunk is allready created
                if (chunks[i] != null)
                {
                    // If the chunk is available
                    if (chunks[i].State == ChunkState.Available) return chunks[i];
                    continue;
                }
                // Else, load data and create chunk
                chunks[i] = new DataChunk<T>(DataSource.Next(ChunkLength), i);
                return chunks[i];
            }
            return null;
        }

    }
}
