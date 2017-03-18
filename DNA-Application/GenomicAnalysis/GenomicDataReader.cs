using NetworkComputeFramework.Data;
using System;
using System.Collections.Generic;

namespace GenomicAnalysis
{
    public class GenomicDataReader : IDataReader<GenomicBase>
    {
        private string[] lines;
        private int cursor;

        public long Length { get; private set; }

        public GenomicDataReader(string[] lines)
        {
            this.lines = lines;
            this.cursor = 1;
            Length = lines.Length - 1;
        }

        public GenomicBase Next()
        {
            lock (GetType()) {
                return Parse(lines[cursor++]);
            }
        }

        public static GenomicBase Parse(string data)
        {
            var tokens = data.Split('\t');
            return new GenomicBase(tokens[0], tokens[1], long.Parse(tokens[2]), tokens[3]);
        }

        public GenomicBase[] Next(int length)
        {
            lock (GetType())
            {
                GenomicBase[] output = new GenomicBase[length];
                for (int i = 0; i < length; i++)
                {
                    output[i++] = Next();
                }
                return output;
            }
        }
    }
}
