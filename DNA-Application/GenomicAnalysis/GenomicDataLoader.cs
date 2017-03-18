using NetworkComputeFramework.Data;
using System;
using System.IO;

namespace GenomicAnalysis
{
    public class GenomicDataLoader : IDataLoader<string, GenomicBase>
    {
        public IDataReader<GenomicBase> Open(string path)
        {
            path = @"C:\Users\Bureau\Documents\Workspace\VisualStudio\C-ProjetDNA\DNA-Data\genome-greshake.txt";
            
            return new GenomicDataReader(File.ReadAllLines(path));
        }
    }
}
