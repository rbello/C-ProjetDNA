using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using System.Collections.Generic;
using System;

namespace NetworkComputeFramework
{
    public abstract class Job<T>
    {
        public IDataReader<T> DataReader { get; protected set; }

        public bool Interrupted { get; set; }

        public Job(IDataReader<T> dataReader)
        {
            DataReader = dataReader;
            Interrupted = false;
            Results = new Dictionary<int, object>();
        }

        public abstract IMapper<T> CreateMapper(int chunkLength);

        public abstract IReducer<T> CreateReducer();

        public IDictionary<int, object> Results { get; private set; }

        public void CleanUp()
        {
            DataReader.Dispose();
        }
    }
}
