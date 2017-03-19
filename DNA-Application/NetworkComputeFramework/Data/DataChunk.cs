
namespace NetworkComputeFramework.Data
{
    public class DataChunk<T>
    {
        public int Id { get; protected set; }

        public ChunkState State { get; set; }

        public T[] Data { get; protected set; }

        public long RealLength {
            get
            {
                if (Data == null) return -1;
                long length = 0;
                for (int i = 0; i < Data.Length; ++i)
                {
                    if (Data[i] != null) ++length;
                }
                return length;
            }
        }

        public DataChunk(T[] data, int id)
        {
            Data = data;
            Id = id;
            State = ChunkState.Available;
        }
    }

    public enum ChunkState { Available, Booked, Done }
}
