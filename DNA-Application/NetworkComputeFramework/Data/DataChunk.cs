using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComputeFramework.Data
{
    public class DataChunk<T>
    {
        public bool Done { get; set; }

        public T[] Data { get; protected set; }

        public DataChunk(T[] data)
        {
            Data = data;
        }
    }
}
