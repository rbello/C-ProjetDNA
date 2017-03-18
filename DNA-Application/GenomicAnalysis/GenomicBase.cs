using System;

namespace GenomicAnalysis
{
    public sealed class GenomicBase
    {
        public readonly string rsid;
        public readonly string chromosome;
        public readonly long position;
        public readonly string genotype;

        public GenomicBase(string rsid, string chromosome, long position, string genotype)
        {
            this.rsid = rsid;
            this.chromosome = chromosome;
            this.position = position;
            this.genotype = genotype;
        }
    }
}
