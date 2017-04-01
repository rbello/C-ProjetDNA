
/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.Data
{

    /// <summary>
    /// A chunk of data.
    /// </summary>
    /// <typeparam name="T">Type of the unitary data chunked</typeparam>
    public class DataChunk<T>
    {

        /// <summary>
        /// Counter incremeted for each chunk.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Return the processing state of this chunk.
        /// </summary>
        public ChunkState State { get; set; }

        /// <summary>
        /// Returns data stored in this chunk.
        /// </summary>
        public T[] Data { get; protected set; }

        /// <summary>
        /// Return the real length of this chunk, ignoring null data.
        /// </summary>
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

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data to store into this chunk</param>
        /// <param name="id">Chunk identifier</param>
        public DataChunk(T[] data, int id)
        {
            Data = data;
            Id = id;
            State = ChunkState.Available;
        }

    }

    /// <summary>
    /// Available states for a data chunk.
    /// </summary>
    public enum ChunkState {

        /// <summary>
        /// This chunk was available for processing.
        /// </summary>
        Available,

        /// <summary>
        /// This chunk was booked for a node a must not be reassigned.
        /// </summary>
        Booked,

        /// <summary>
        /// All data of this chunk are processed.
        /// </summary>
        Done

    }

}
