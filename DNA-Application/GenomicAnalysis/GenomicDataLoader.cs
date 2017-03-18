using NetworkComputeFramework.Data;
using System;

namespace GenomicAnalysis
{
    public class GenomicDataLoader : IDataLoader<string, GenomicBase>
    {
        public IDataReader<GenomicBase> Open(string path)
        {
            return new GenomicDataReader();
        }
    }
}
