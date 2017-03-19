using System.Collections.Generic;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using System.Linq;
using System;

namespace GenomicAnalysis.Process
{
    public class StatsProcess : DataProcess<GenomicBase>
    {
        public StatsProcess(IDataReader<GenomicBase> dataReader) : base(dataReader)
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
            return new StatsReducer();
        }
    }

    public class StatsReducer : IReducer<GenomicBase>
    {
        public object Reduce(DataChunk<GenomicBase> chunk)
        {
            Random rnd = new Random();
            return new StatesResult()
            {
                CountBasesA = (int)(rnd.Next() * 0.0004),
                CountBasesT = (int)(rnd.Next() * 0.0006),
                CountBasesG = (int)(rnd.Next() * 0.0005),
                CountBasesC = (int)(rnd.Next() * 0.0003),
                CountBasesUnknown = (int)(rnd.Next() * 0.00001)
            };
        }

        public object Reduce(IDictionary<int, object> subResults)
        {
            var result = new StatesResult();
            foreach (var item in subResults.OrderBy(item => item.Key))
            {
                result += (StatesResult)item.Value;
            }
            return result;
        }
    }

    public class StatesResult
    {
        public long CountBasesA { get; internal set; }
        public long CountBasesT { get; internal set; }
        public long CountBasesG { get; internal set; }
        public long CountBasesC { get; internal set; }
        public long CountBasesUnknown { get; internal set; }
        public long CountBases {
            get
            {
                return CountBasesA + CountBasesT + CountBasesG + CountBasesC + CountBasesUnknown;
            }
        }

        public static StatesResult operator +(StatesResult left, StatesResult right)
        {
            left.CountBasesA += right.CountBasesA;
            left.CountBasesT += right.CountBasesT;
            left.CountBasesG += right.CountBasesG;
            left.CountBasesC += right.CountBasesC;
            left.CountBasesUnknown += right.CountBasesUnknown;
            return left;
        }

        public override string ToString()
        {
            double total = CountBases;
            double percentBaseA = total > 0 ? Math.Round(CountBasesA / total * 100d, 2) : 0;
            double percentBaseT = total > 0 ? Math.Round(CountBasesT / total * 100d, 2) : 0;
            double percentBaseG = total > 0 ? Math.Round(CountBasesG / total * 100d, 2) : 0;
            double percentBaseC = total > 0 ? Math.Round(CountBasesC / total * 100d, 2) : 0;
            double percentBaseUnknown = total > 0 ? Math.Round(CountBasesUnknown / total * 100d, 2) : 0;
            return string.Format("DNA sequence statistics:\r\n   A = {0} ({1}%)\r\n   T = {2} ({3}%)\r\n   G = {4} ({5}%)\r\n   C = {6} ({7}%)\r\n   Unknown = {8} ({9}%)\r\n   Total = {10}",
                CountBasesA, percentBaseA, CountBasesT, percentBaseT, CountBasesG, percentBaseG, CountBasesC, percentBaseC, CountBasesUnknown, percentBaseUnknown, total);
        }

    }

}
