using NetworkComputeFramework;
using System;
using NetworkComputeFramework.Data;

namespace GenomicAnalysis.Jobs
{
    public class CountBasesJob : Job<GenomicBase>
    {
        public CountBasesJob(IDataReader<GenomicBase> dataReader) : base(dataReader)
        {
        }
    }
}
