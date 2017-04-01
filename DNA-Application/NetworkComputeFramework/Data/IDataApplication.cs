
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
    /// A data process application. An application is a practical case of data processing, like
    /// DNA analysis. It provides the business process to apply to the data.
    /// </summary>
    /// <typeparam name="S">Type of the input object giving the location of the data to process</typeparam>
    /// <typeparam name="T">Type of the unitary data processed</typeparam>
    public interface IDataApplication<S, T>
    {

        /// <summary>
        /// Create a data loader allowing to read data.
        /// </summary>
        IDataLoader<S, T> CreateDataLoader();

        /// <summary>
        /// Create a data process, according to given type name.
        /// This method implments the Factory design pattern.
        /// This method should throws an ArgumentException if the common name
        /// of the process is not handled by this application.
        /// </summary>
        /// <param name="processTypeName">The common name of the process to create</param>
        /// <param name="dataReader">The data reader</param>
        DataProcess<T> CreateProcess(string processTypeName, IDataReader<T> dataReader);

        /// <summary>
        /// Returns the list of handled process common names.
        /// </summary>
        string[] GetAvailableProcessTypes();

    }
}
