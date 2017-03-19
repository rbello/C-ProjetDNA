using System.Collections.Generic;
using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using System.Linq;
using System;

namespace GenomicAnalysis.Process
{
    public class StatsProcess : DataProcess<GenomicNucleotidePeer>
    {
        public StatsProcess(IDataReader<GenomicNucleotidePeer> dataReader) : base(dataReader)
        {
            // Specify witch datareader to use
        }

        public override IMapper<GenomicNucleotidePeer> CreateMapper(int chunkLength)
        {
            // Use a standard mapper
            return new RegularChunkMapper<GenomicNucleotidePeer>(chunkLength, DataReader);
        }

        public override IReducer<GenomicNucleotidePeer> CreateReducer()
        {
            // Use a specific reducer
            return new StatsReducer();
        }
    }

    public class StatsReducer : IReducer<GenomicNucleotidePeer>
    {
        public object Reduce(DataChunk<GenomicNucleotidePeer> chunk)
        {
            // Prepare a result object
            var result = new StatesResult();
            // Using Parallel library to fetch chunk efficiently
            // Not used because the ForEach method is asynchronous, so the result may not
            // be computed totally when process ends. The final counts looks like random data!
            /*System.Threading.Tasks.Parallel.ForEach(chunk.Data, delegate (GenomicBase gb)
            {
                if (gb.genotype == null) return;
                if (gb.genotype.Length > 0) CountGenomicBase(gb.genotype[0], result);
                if (gb.genotype.Length > 1) CountGenomicBase(gb.genotype[1], result);
            });*/
            foreach (var gb in chunk.Data)
            {
                if (gb.genotype == null) continue;
                if (gb.genotype.Length > 0) CountGenomicBase(gb.genotype[0], result);
                if (gb.genotype.Length > 1) CountGenomicBase(gb.genotype[1], result);
                result.CountGenotype++;
            }
            return result;
        }

        private static void CountGenomicBase(char genotype, StatesResult result)
        {
            switch (genotype)
            {
                case 'A': result.CountBasesA++; break;
                case 'T': result.CountBasesT++; break;
                case 'G': result.CountBasesG++; break;
                case 'C': result.CountBasesC++; break;
                default:  result.CountBasesUnknown++; break;
            }
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
        public long CountGenotype { get; internal set; }
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
            left.CountGenotype += right.CountGenotype;
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
            return string.Format("DNA sequence statistics:\r\n   A = {0} ({1}%)\r\n   T = {2} ({3}%)\r\n   G = {4} ({5}%)\r\n   C = {6} ({7}%)\r\n   Unknown = {8} ({9}%)\r\n   Total bases = {10}\r\n   Total genotypes = {11}",
                CountBasesA, percentBaseA, CountBasesT, percentBaseT, CountBasesG, percentBaseG, CountBasesC, percentBaseC, CountBasesUnknown, percentBaseUnknown, total, CountGenotype);
        }

    }

}
