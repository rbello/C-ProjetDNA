using System;
using NetworkComputeFramework;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using NetworkComputeFramework.Worker;

namespace GenomicAnalysis.Jobs
{
    public class CountBasesJob : Job<GenomicBase>
    {
        public CountBasesJob(IDataReader<GenomicBase> dataReader) : base(dataReader)
        {
        }

        public override IMapper<GenomicBase> CreateMapper(int chunkLength)
        {
            return new RegularChunkMapper<GenomicBase>(chunkLength, DataReader);
        }
    }
}
