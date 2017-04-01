using System;

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
    /// A data reader is used to fetch data from data source.
    /// It was created by a IDataLoader.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataReader<T> : IDisposable
    {

        /// <summary>
        /// Returns the total data counts.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Returns the next element in the data store.
        /// This method can throw an IndexOutOfRangeException is the end of
        /// data was reached.
        /// </summary>
        T Next();

        /// <summary>
        /// Returns the N next elements in the data store.
        /// This method can throw an IndexOutOfRangeException is the end of
        /// data was reached.
        /// </summary>
        T[] Next(int length);

        /// <summary>
        /// Returns false is the reader was consummed.
        /// </summary>
        bool HasNext { get; }

    }
}
