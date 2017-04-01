using System;
using System.Collections.Generic;
using NetworkComputeFramework.Data;

/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.MapReduce
{

    /// <summary>
    /// A simple chunk mapper, dividing data as fixed-length chunks.
    /// </summary>
    /// <typeparam name="T">Type of the unitary data chunked</typeparam>
    public class RegularChunkMapper<T> : IMapper<T>
    {
        private DataChunk<T>[] chunks;

        public RegularChunkMapper(int chunkLength, IDataReader<T> dataSource)
        {
            DataSource = dataSource;
            ChunkPreferredLength = chunkLength;
            ChunkCount = (int)(DataSource.Length / ChunkPreferredLength);
            ChunkRemainsLength = (int)(DataLength - ChunkCount * ChunkPreferredLength);
            if (ChunkRemainsLength > 0) ChunkCount++;
            chunks = new DataChunk<T>[ChunkCount];
        }

        public IDataReader<T> DataSource { get; protected set; }

        public long DataLength => DataSource.Length;

        public int ChunkPreferredLength { get; private set; }

        public int ChunkRemainsLength { get; private set; }

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
            ChunkPreferredLength = ChunkRemainsLength = ChunkCount = 0;
            DataSource = null;
        }

        [Obsolete]
        public IEnumerator<DataChunk<T>> GetEnumerator()
        {
            for (int i = 0; i < ChunkCount; ++i)
            {
                yield return new DataChunk<T>(DataSource.Next(ChunkPreferredLength), i);
            }
            if (ChunkRemainsLength > 0) yield return new DataChunk<T>(DataSource.Next(ChunkRemainsLength), ChunkCount);
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
                    // Else, continue to next chunk
                    continue;
                }
                // Else, load data and create chunk
                chunks[i] = new DataChunk<T>(DataSource.Next(ChunkLength(i)), i);
                return chunks[i];
            }
            // All chunks created and not available
            return null;
        }

        private int ChunkLength(int chunkId)
        {
            if (ChunkRemainsLength == 0) return ChunkPreferredLength;
            return chunkId == ChunkCount - 1 ? ChunkRemainsLength : ChunkPreferredLength;
        }
    }
}
