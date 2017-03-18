using NetworkComputeFramework.Data;
using System;
using NetworkComputeFramework;
using GenomicAnalysis.Jobs;
using NetworkComputeFramework.MapReduce;

namespace GenomicAnalysis
{
    public class GenomicAnalysisApplication : IDataApplication<string, GenomicBase>
    {
        public IDataLoader<string, GenomicBase> CreateDataLoader()
        {
            return new GenomicDataLoader();
        }

        public Job<GenomicBase> CreateJob(string jobTypeName, IDataReader<GenomicBase> dataReader)
        {
            switch (jobTypeName)
            {
                case "COUNT_BASES":  return new CountBasesJob(dataReader);
            }
            throw new ArgumentException(string.Format("Job type '{0}' is not supported by '{1}' application",
                jobTypeName, GetType().Name));
        }

        public string[] GetAvailableJobTypes()
        {
            return new[] {
                "COUNT_BASES"
            };
        }
    }
}
