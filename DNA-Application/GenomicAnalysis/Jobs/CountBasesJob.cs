using NetworkComputeFramework;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;

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

        public override IReducer<GenomicBase> CreateReducer()
        {
            return new CountBasesReducer();
        }
    }

    public class CountBasesReducer : IReducer<GenomicBase>
    {
        public void Reduce(DataChunk<GenomicBase> chunk)
        {
            chunk.Done = true;
        }
    }
}
