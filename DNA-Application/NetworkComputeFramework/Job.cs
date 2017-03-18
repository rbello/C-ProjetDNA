using NetworkComputeFramework.Data;
using System;

namespace NetworkComputeFramework
{
    public abstract class Job<T>
    {
        public IDataReader<T> DataReader { get; protected set; }

        public Job(IDataReader<T> dataReader)
        {
            DataReader = dataReader;
        }
    }
}
