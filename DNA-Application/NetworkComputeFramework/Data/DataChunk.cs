
namespace NetworkComputeFramework.Data
{
    public class DataChunk<T>
    {
        public int Id { get; protected set; }

        public ChunkState State { get; set; }

        public T[] Data { get; protected set; }

        public DataChunk(T[] data, int id)
        {
            Data = data;
            Id = id;
            State = ChunkState.Available;
        }
    }

    public enum ChunkState { Available, Booked, Done }
}
