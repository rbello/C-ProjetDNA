using System;

namespace NetworkComputeFramework.Data
{
    public interface IDataReader<T> : IDisposable
    {

        long Length { get; }

        T Next();

        T[] Next(int length);

        bool HasNext { get; }

    }
}
