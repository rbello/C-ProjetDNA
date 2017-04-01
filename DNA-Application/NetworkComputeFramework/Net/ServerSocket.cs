using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/// ██████╗ ███╗   ██╗ █████╗     ██████╗ ██████╗  ██████╗      ██╗███████╗ ██████╗████████╗
/// ██╔══██╗████╗  ██║██╔══██╗    ██╔══██╗██╔══██╗██╔═══██╗     ██║██╔════╝██╔════╝╚══██╔══╝
/// ██║  ██║██╔██╗ ██║███████║    ██████╔╝██████╔╝██║   ██║     ██║█████╗  ██║        ██║   
/// ██║  ██║██║╚██╗██║██╔══██║    ██╔═══╝ ██╔══██╗██║   ██║██   ██║██╔══╝  ██║        ██║   
/// ██████╔╝██║ ╚████║██║  ██║    ██║     ██║  ██║╚██████╔╝╚█████╔╝███████╗╚██████╗   ██║   
/// ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝    ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝  
/// 
/// Copyleft 2017 https://github.com/rbello/C-ProjetDNA
namespace NetworkComputeFramework.Net
{
    /// <summary>
    /// A server socket, able to accept connections of remote workers.
    /// 
    /// See: https://msdn.microsoft.com/fr-fr/library/fx6588te(v=vs.110).aspx
    /// </summary>
    public class ServerSocket : IDisposable
    {
        internal static readonly string EOF = "<EOF>";

        public Action<string> Logger { get; private set; }
        public int BacklogLength { get; set; } = 100;
        public int LocalPort { get; private set; }
        public IPAddress LocalAddress { get; private set; }
        public Socket LocalSocket { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }
        public bool IsDisposed { get; private set; }

        public event Action<Exception, string> OnSocketError;
        public event Action<Client> OnClientConnected;
        public event Action<Client, string> OnMessageReceived;

        internal ManualResetEvent allDone = new ManualResetEvent(false);

        internal IList<Client> clients;
        private Thread thread;

        public ServerSocket(int portNumber, Action<string> logger = null)
        {
            clients = new List<Client>();
            LocalPort = portNumber;
            Logger = logger;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            LocalAddress = ipHostInfo.AddressList[0];
            LocalAddress = IPAddress.Loopback; // TODO: 
            LocalEndPoint = new IPEndPoint(LocalAddress, portNumber);
            LocalSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Bind()
        {
            thread = new Thread(delegate ()
            {
                BindSynch();
            });
            thread.Start();
        }

        public void BindSynch()
        {
            Log("Preparing to open port " + LocalAddress + ":" + LocalPort);
            try
            {
                LocalSocket.Bind(LocalEndPoint);
                LocalSocket.Listen(100);

                Log("Listening on localhost:" + LocalPort);

                // Bind the socket to the local endpoint and listen for incoming connections
                while (true)
                {
                    // Set the event to nonsignaled state
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections
                    LocalSocket.BeginAccept(new AsyncCallback(AcceptCallback), LocalSocket);

                    // Wait until a connection is made before continuing
                    allDone.WaitOne();
                }

            }
            catch (Exception ex)
            {
                if (!IsDisposed) OnSocketError?.Invoke(ex, "BindOrListen");
            }
        }

        public void Send(Client clientSocket, string data)
        {
            clientSocket.Send(data);
        }

        internal void Log(string msg)
        {
            Logger?.Invoke(msg);
        }

        internal void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue
            allDone.Set();

            // Get the socket that handles the client request
            Socket listener = (Socket)ar.AsyncState;
            try
            {
                Socket client = listener.EndAccept(ar);

                // Create the state object
                var state = new Client(this, client);

                // Save client reference
                clients.Add(state);

                // Send event
                if (!IsDisposed) OnClientConnected?.Invoke(state);

                // Log
                Log("New client connected:" + state);

                // Enable receive
                //client.BeginReceive(state.buffer, 0, ClientSocket.BufferSize, 0,
                //    new AsyncCallback(ReadCallback), state);

                state.StartReceive();

            }
            catch (Exception ex)
            {
                if (!IsDisposed) OnSocketError?.Invoke(ex, "Accept");
            }
        }

        internal void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object
            Client state = (Client)ar.AsyncState;
            Socket handler = state.RemoteSocket;

            // Socket is disposed
            if (handler == null) return;

            // Read data from the client socket
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read more data
                content = state.sb.ToString();

                if (content.IndexOf(EOF) > -1)
                {
                    // All the data has been read from the client. Display it on the logger
                    Log(string.Format("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content));
                    // Send event
                    if (!IsDisposed) OnMessageReceived?.Invoke(state, content);
                }
                else
                {
                    // Not all data received. Get more
                    handler.BeginReceive(state.buffer, 0, Client.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
            }

            
        }

        internal void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device
                int bytesSent = handler.EndSend(ar);
                

                //TODO: handler.Shutdown(SocketShutdown.Both);
                //TODO: handler.Close();

            }
            catch (Exception ex)
            {
                if (!IsDisposed) OnSocketError?.Invoke(ex, "Write");
            }
        }

        public void Dispose()
        {
            IsDisposed = true;
            Logger = null;
            // Close clients
            foreach (Client client in clients) {
                client.Dispose();
            }
            LocalSocket.Close();
            LocalSocket.Dispose();
            LocalSocket = null;
        }

        internal void MessageReceived(Client clientSocket, string content)
        {
            OnMessageReceived?.Invoke(clientSocket, content);
        }

        public void SendAll(string message, Action<Exception> callback)
        {
            //TODO: Implements SendAll
        }
    }

    // State object for reading client data asynchronously
    public class Client
    {
        // Sockets
        public ServerSocket LocalSocket { get; private set; }
        public Socket RemoteSocket { get; private set; }

        // Size of receive buffer
        public const int BufferSize = 1024;

        // Receive buffer
        public byte[] buffer = new byte[BufferSize];

        // Received data string
        public StringBuilder sb = new StringBuilder();

        internal bool Active = true;

        // Constructor
        public Client(ServerSocket serverSocket, Socket clientSocket)
        {
            LocalSocket = serverSocket;
            RemoteSocket = clientSocket;
        }

        public void Send(string data)
        {
            // Log
            LocalSocket.Log("Send " + data.Length + " chars to client " + this);

            // Convert the string data to byte data using ASCII encoding
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device
            RemoteSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(LocalSocket.SendCallback), RemoteSocket);
        }

        public override string ToString()
        {
            return "Client[" + RemoteSocket.RemoteEndPoint.ToString() + "]";
        }

        internal void Dispose()
        {
            Active = false;
            RemoteSocket.Close();
            RemoteSocket.Dispose();
            RemoteSocket = null;
        }

        internal void StartReceive()
        {
            new Thread(delegate ()
            {
                try
                {
                    while (Active)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = RemoteSocket.Receive(bytes);
                        string content = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        LocalSocket.MessageReceived(this, content);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace); // TODO:
                }
            }).Start();
        }
    }

}
