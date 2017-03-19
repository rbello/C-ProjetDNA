namespace GenomicAnalysis
{
    public sealed class GenomicNucleotidePeer
    {
        public readonly string rsid;
        public readonly string chromosome;
        public readonly long position;
        public readonly string genotype;

        public GenomicNucleotidePeer(string rsid, string chromosome, long position, string genotype)
        {
            this.rsid = rsid;
            this.chromosome = chromosome;
            this.position = position;
            this.genotype = genotype;
        }
    }
}
