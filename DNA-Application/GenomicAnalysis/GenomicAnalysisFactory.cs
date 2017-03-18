using NetworkComputeFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkComputeFramework;
using GenomicAnalysis.Jobs;

namespace GenomicAnalysis
{
    public class GenomicAnalysisFactory : IFactory<string, GenomicBase>
    {
        public IDataLoader<string, GenomicBase> CreateDataLoader()
        {
            return new GenomicDataLoader();
        }

        public Job<GenomicBase> CreateJob(string jobTypeName, IDataReader<GenomicBase> dataReader)
        {
            switch (jobTypeName)
            {
                case "COUNT_BASES": return new CountBasesJob(dataReader);
            }
            throw new ArgumentException(string.Format("Job type '{0}' is not supported by '{1}' abstract factory",
                jobTypeName, GetType().Name));
        }
    }
}
