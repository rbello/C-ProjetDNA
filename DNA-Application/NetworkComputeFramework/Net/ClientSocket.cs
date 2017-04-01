using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkComputeFramework.Net
{
    public class ClientSocket
    {
        public Socket Sock { get; private set; }

        public Action<string> Logger { get; private set; }

        public ClientSocket(Action<string> logger = null)
        {
            Logger = logger;
            
        }

        public bool Connect(string address, int port)
        {
            IPAddress ip = IPAddress.Parse(address);
            IPEndPoint ipEnd = new IPEndPoint(ip, port);
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sock.Connect(ipEnd);
            if (Sock.Connected)
            {
                Logger?.Invoke("Socket connected from " + Sock.LocalEndPoint + " to " + address + ":" + port);
            }
            else
            {
                Logger?.Invoke("Unable to connect socket to " + address + ":" + port);
            }
            return Sock.Connected;
        }

        public void Connect(string address, int port, Action<ClientSocket, bool, Exception> callback)
        {
            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    Connect(address, port);
                    callback?.Invoke(this, Sock.Connected, null);
                    PollSocketInput();
                }
                catch (Exception ex)
                {
                    callback?.Invoke(this, Sock.Connected, ex);
                }

            })).Start();
        }

        /// <summary>
        /// Asynchronous method.
        /// </summary>
        public void Send(string message, Action<int, Exception> callback)
        {
            Send(Encoding.UTF8.GetBytes(message), callback);
        }

        /// <summary>
        /// Synchronous method.
        /// </summary>
        public int Send(string message)
        {
            return Send(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Asynchronous method.
        /// </summary>
        public void Send(byte[] message, Action<int, Exception> callback)
        {
            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    int result = Sock.Send(message, message.Length, SocketFlags.None);
                    callback(result, null);
                }
                catch (Exception ex)
                {
                    callback(0, ex);
                }
            })).Start();
        }

        /// <summary>
        /// Synchronous method.
        /// </summary>
        public int Send(byte[] message)
        {
            return Sock.Send(message, message.Length, SocketFlags.None);
        }

        private void PollSocketInput()
        {
            Logger?.Invoke("Socket start polling...");
            while (Sock.Connected)
            {
                if (Sock.Poll(10, SelectMode.SelectRead) && Sock.Available == 0)
                {
                    //TODO: Disconnected
                    Thread.CurrentThread.Abort();
                    break;
                }
                if (Sock.Available > 0)
                {
                    while (Sock.Available > 0)
                    {
                        byte[] msg = new Byte[Sock.Available];
                        Sock.Receive(msg, 0, Sock.Available, SocketFlags.None);
                        Logger?.Invoke("Socket received " + Sock.Available + " bytes");
                    }
                }
            }
            Logger?.Invoke("Socket stop polling!");
        }
    }
}
