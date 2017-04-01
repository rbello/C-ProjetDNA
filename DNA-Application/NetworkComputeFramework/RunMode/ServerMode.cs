using NetworkComputeFramework.Data;
using NetworkComputeFramework.Net;
using NetworkComputeFramework.Node;
using System;

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
    /// Implementation of a server application, able to accept remote connections from
    /// remote nodes over the network, and to distribute the data process.
    /// </summary>
    /// <typeparam name="S">Type of the input object giving the location of the data to process</typeparam>
    /// <typeparam name="T">Type of the unitary data processed</typeparam>
    public class ServerMode<S, T> : AbstractRunMode
    {

        protected IDataApplication<S, T> application;

        private ServerSocket serverSocket;

        public DataProcess<T> CurrentProcess { get; private set; }

        public ServerMode(IDataApplication<S, T> application, Action<string> logger) : base(logger)
        {
            this.application = application;
        }

        protected override void Init(params object[] args)
        {
            CreateServerSocket(Convert.ToInt32(args[0]));
            RunLocalNode(Convert.ToInt32(args[1]));
            ChangeState(RunState.IDLE);
        }

        private void CreateServerSocket(int portNumber)
        {
            serverSocket = new ServerSocket(portNumber, Logger);
            serverSocket.OnSocketError += delegate (Exception error, string source)
            {
                Logger.Invoke("Socket error " + error.GetType().Name + " from " + source + ": " + error.Message);
            };
            serverSocket.OnClientConnected += delegate (Client clientSocket)
            {
                Logger.Invoke("Client connected: " + clientSocket);
                var node = new RemoteNode(clientSocket);
                WorkerPool.AddNode(node);
            };
            serverSocket.OnMessageReceived += delegate (Client clientSocket, string message)
            {
                Logger.Invoke("Message received from " + clientSocket + ": " + message);
            };
            serverSocket.Bind();
        }

        private void RunLocalNode(int localThreadsCount)
        {
            WorkerPool.AddNode(new LocalNode(localThreadsCount));
        }

        protected override void StartSynch(Action<object> success, Action<Exception> failure, params object[] args)
        {
            // Change run mode state
            ChangeState(RunState.LOAD_BEGIN);

            // Open data source
            IDataLoader<S, T> loader = application.CreateDataLoader();
            IDataReader<T> reader = loader.Open((S) args[0]);

            // Create the process
            CurrentProcess = application.CreateProcess((string)args[1], reader);

            // Run the process into the workers' pool
            WorkerPool.Process(
                // Process to execute
                CurrentProcess,
                // On success
                delegate (object finalResult)
                {
                    CurrentProcess.CleanUp();
                    ChangeState(RunState.REDUCE_DONE);
                    success.Invoke(finalResult);
                },
                // On failure
                delegate (Exception ex)
                {
                    CurrentProcess.CleanUp();
                    ChangeState(RunState.FAILURE);
                    LastException = ex;
                    failure.Invoke(ex);
                });
        }

        public override void Stop()
        {
            if (CurrentProcess != null)
            {
                CurrentProcess.Interrupted = true;
            }
            serverSocket.Dispose();
        }
    }
}
