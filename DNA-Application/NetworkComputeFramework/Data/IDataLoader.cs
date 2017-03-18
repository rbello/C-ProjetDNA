using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComputeFramework.Data
{
    public interface IDataLoader<S, T>
    {
        IDataReader<T> Open(S path);
    }
}
