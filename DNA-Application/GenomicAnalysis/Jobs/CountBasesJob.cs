using NetworkComputeFramework;
using System;
using NetworkComputeFramework.Data;

namespace GenomicAnalysis.Jobs
{
    public class CountBasesJob : Job<GenomicBase>
    {
        private IDataReader<GenomicBase> dataReader;

        public CountBasesJob(IDataReader<GenomicBase> dataReader)
        {
            this.dataReader = dataReader;
        }
    }
}
