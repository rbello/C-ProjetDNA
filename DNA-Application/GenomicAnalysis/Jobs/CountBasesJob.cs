using System;
using System.Collections.Generic;
using NetworkComputeFramework;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;

namespace GenomicAnalysis.Jobs
{
    public class CountBasesJob : Job<GenomicBase>
    {
        public CountBasesJob(IDataReader<GenomicBase> dataReader) : base(dataReader)
        {
            // Specify witch datareader to use
        }

        public override IMapper<GenomicBase> CreateMapper(int chunkLength)
        {
            // Use a standard mapper
            return new RegularChunkMapper<GenomicBase>(chunkLength, DataReader);
        }

        public override IReducer<GenomicBase> CreateReducer()
        {
            // Use a specific reducer
            return new CountBasesReducer();
        }
    }

    public class CountBasesReducer : IReducer<GenomicBase>
    {
        public object Reduce(DataChunk<GenomicBase> chunk)
        {
            return new CountBasesResult();
        }

        public object Reduce(IDictionary<int, object> subResults)
        {
            return "YES";
        }
    }

    public class CountBasesResult
    {

    }

}
