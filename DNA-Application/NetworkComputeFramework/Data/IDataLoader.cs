namespace NetworkComputeFramework.Data
{
    public interface IDataLoader<S, T>
    {
        IDataReader<T> Open(S path);
    }
}
