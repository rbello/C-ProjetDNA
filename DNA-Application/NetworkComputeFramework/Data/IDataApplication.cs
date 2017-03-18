using NetworkComputeFramework.MapReduce;
using System;

namespace NetworkComputeFramework.Data
{
    public interface IDataApplication<S, T>
    {

        IDataLoader<S, T> CreateDataLoader();

        Job<T> CreateJob(string jobTypeName, IDataReader<T> dataReader);

        IMapper CreateMapper();

        string[] GetAvailableJobTypes();
    }
}
