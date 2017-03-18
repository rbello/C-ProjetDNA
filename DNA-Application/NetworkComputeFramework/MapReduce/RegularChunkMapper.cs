using System;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.Worker;

namespace NetworkComputeFramework.MapReduce
{
    public class RegularChunkMapper<T> : IMapper<T>
    {
        private WorkerPool workerPool;

        public RegularChunkMapper(WorkerPool workerPool, IDataReader<T> dataSource)
        {
            this.workerPool = workerPool;
            this.DataSource = dataSource;
        }

        public IDataReader<T> DataSource { get; protected set; }

        public void Map(Action<int, long, long, long, T[]> chunkIterator)
        {
            int chunkCount   = workerPool.WorkersCount;
            long dataLength  = DataSource.Length;
            long chunkLength = (long) Math.Floor((double)(dataLength / chunkCount));
            long remains     = dataLength - chunkCount * chunkLength;
            for (int i = 0; i < chunkCount; ++i)
            {
                chunkIterator.Invoke(i, i * chunkLength, ((i + 1) * chunkLength) - 1, chunkLength, null);
            }
            if (remains > 0)
            {
                chunkIterator.Invoke(chunkCount, chunkCount * chunkLength, dataLength - 1, remains, null);
            }
        }
    }
}
