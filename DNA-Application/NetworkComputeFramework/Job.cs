using NetworkComputeFramework.Data;
using System;
using NetworkComputeFramework.MapReduce;
using NetworkComputeFramework.Worker;

namespace NetworkComputeFramework
{
    public abstract class Job<T>
    {
        public IDataReader<T> DataReader { get; protected set; }

        public Job(IDataReader<T> dataReader)
        {
            DataReader = dataReader;
        }

        public abstract IMapper<T> CreateMapper(int chunkLength);
        public abstract IReducer<T> CreateReducer();
    }
}
