using System.Collections.Generic;
using NetworkComputeFramework.Data;

namespace NetworkComputeFramework.MapReduce
{
    public interface IReducer<T>
    {

        object Reduce(DataChunk<T> chunk);

        object Reduce(IDictionary<int, object> subResults);

    }
}
