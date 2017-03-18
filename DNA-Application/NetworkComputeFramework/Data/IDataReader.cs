using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComputeFramework.Data
{
    public interface IDataReader<T>
    {

        int Length { get; }

        T Next();

        T[] Next(int length);

    }
}
