using NetworkComputeFramework.MapReduce;
using System.Collections.Generic;

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
    /// Abstract class for a data process. It implements the Strategy design pattern.
    /// </summary>
    /// <typeparam name="T">Type of the unitary data processed</typeparam>
    public abstract class DataProcess<T>
    {

        /// <summary>
        /// Get the data reader, an object allowing to access data.
        /// </summary>
        public IDataReader<T> DataReader { get; protected set; }

        /// <summary>
        /// Flag to rise if this process must be interrupted.
        /// </summary>
        public bool Interrupted { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        public DataProcess(IDataReader<T> dataReader)
        {
            DataReader = dataReader;
            Interrupted = false;
            Results = new Dictionary<int, object>();
        }

        /// <summary>
        /// Create a mapper, used to chunk data.
        /// </summary>
        /// <param name="chunkLength">How many items in each chunk</param>
        /// <returns></returns>
        public abstract IMapper<T> CreateMapper(int chunkLength);

        /// <summary>
        /// Create a reducer, used to reduce data as a valuable information.
        /// </summary>
        /// <returns></returns>
        public abstract IReducer<T> CreateReducer();

        /// <summary>
        /// Store the reduced results of each chunk.
        /// Dictionnary type parameters are :
        ///   - int represents the chunk id
        ///   - object is the processing result
        /// </summary>
        public IDictionary<int, object> Results { get; private set; }

        /// <summary>
        /// Dispose this process and free memory. This method will also dispose datareader.
        /// </summary>
        public void CleanUp()
        {
            //TODO: Replace with IDisposable implementation ?
            DataReader.Dispose();
        }
    }
}
