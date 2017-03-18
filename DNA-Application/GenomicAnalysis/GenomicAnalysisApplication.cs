using GenomicAnalysis.Process;
using NetworkComputeFramework.Data;
using System;

namespace GenomicAnalysis
{
    public class GenomicAnalysisApplication : IDataApplication<string, GenomicBase>
    {
        public IDataLoader<string, GenomicBase> CreateDataLoader()
        {
            return new GenomicDataLoader();
        }

        public DataProcess<GenomicBase> CreateProcess(string processTypeName, IDataReader<GenomicBase> dataReader)
        {
            switch (processTypeName)
            {
                case "COUNT_BASES":  return new CountBasesProcess(dataReader);
            }
            throw new ArgumentException(string.Format("Process type '{0}' is not supported by '{1}' application",
                processTypeName, GetType().Name));
        }

        public string[] GetAvailableProcessTypes()
        {
            return new[] {
                "COUNT_BASES"
            };
        }
    }
}
