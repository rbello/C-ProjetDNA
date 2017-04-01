
/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.RunMode
{

    /// <summary>
    /// All states a run mode can have.
    /// </summary>
    public enum RunState
    {
        /// <summary>
        /// The run mode is awaiting for instructions to begin process.
        /// </summary>
        IDLE,

        /// <summary>
        /// Data are loading.
        /// </summary>
        LOAD_BEGIN,

        /// <summary>
        /// Data are loaded.
        /// </summary>
        LOAD_DONE,

        /// <summary>
        /// Data are currently chunked (map).
        /// </summary>
        MAP_BEGIN,

        /// <summary>
        /// All data are chunked (map).
        /// </summary>
        MAP_DONE,

        /// <summary>
        /// The reduce process has begin.
        /// </summary>
        REDUCE_BEGIN,

        /// <summary>
        /// The reduce process is finished.
        /// </summary>
        REDUCE_DONE,

        /// <summary>
        /// An exception was throws during the process.
        /// </summary>
        FAILURE

    }
}
