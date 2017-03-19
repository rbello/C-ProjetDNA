using System;
using NetworkComputeFramework.Data;

namespace GenomicAnalysis
{
    public class GenomicDataReader : IDataReader<GenomicNucleotidePeer>
    {
        private string[] data;

        private int cursor;

        public long Length { get; private set; }

        public bool HasNext => cursor < Length;

        public GenomicDataReader(string[] data)
        {
            this.data = data;
            this.cursor = 1;
            Length = data.Length - 1;
        }

        public GenomicNucleotidePeer Next()
        {
            lock (GetType()) {
                return Parse(data[cursor++]);
            }
        }

        public static GenomicNucleotidePeer Parse(string data)
        {
            var tokens = data.Split('\t');
            return new GenomicNucleotidePeer(tokens[0], tokens[1], long.Parse(tokens[2]), tokens[3]);
        }

        public GenomicNucleotidePeer[] Next(int length)
        {
            lock (GetType())
            {
                GenomicNucleotidePeer[] output = new GenomicNucleotidePeer[length];
                for (int i = 0; i < length; i++)
                {
                    output[i] = Next();
                }
                return output;
            }
        }

        public void Dispose()
        {
            data = null;
            cursor = -1;
            Length = 0;
        }
    }
}
