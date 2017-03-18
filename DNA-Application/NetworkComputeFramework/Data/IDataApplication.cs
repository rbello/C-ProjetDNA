using NetworkComputeFramework.MapReduce;
using System;

namespace NetworkComputeFramework.Data
{
    public interface IDataApplication<S, T>
    {

        IDataLoader<S, T> CreateDataLoader();

        DataProcess<T> CreateProcess(string processTypeName, IDataReader<T> dataReader);

        string[] GetAvailableProcessTypes();
    }
}
