using System.Collections.Generic;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;

namespace GenomicAnalysis.Process
{
    public class CountBasesProcess : DataProcess<GenomicBase>
    {
        public CountBasesProcess(IDataReader<GenomicBase> dataReader) : base(dataReader)
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
