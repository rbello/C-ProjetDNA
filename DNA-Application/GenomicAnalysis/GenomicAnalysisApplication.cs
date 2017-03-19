using GenomicAnalysis.Process;
using NetworkComputeFramework.Data;
using System;

namespace GenomicAnalysis
{
    public class GenomicAnalysisApplication : IDataApplication<string, GenomicNucleotidePeer>
    {
        public IDataLoader<string, GenomicNucleotidePeer> CreateDataLoader()
        {
            return new GenomicDataLoader();
        }

        public DataProcess<GenomicNucleotidePeer> CreateProcess(string processTypeName, IDataReader<GenomicNucleotidePeer> dataReader)
        {
            switch (processTypeName)
            {
                case "COUNT_BASES":  return new StatsProcess(dataReader);
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
