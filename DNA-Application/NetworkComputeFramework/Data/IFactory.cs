using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkComputeFramework;

namespace NetworkComputeFramework.Data
{
    public interface IFactory<S, T>
    {
        IDataLoader<S, T> CreateDataLoader();
        Job<T> CreateJob(string jobTypeName, IDataReader<T> dataReader);
    }
}
