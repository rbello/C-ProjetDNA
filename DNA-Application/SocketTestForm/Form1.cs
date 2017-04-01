using NetworkComputeFramework.Net;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SocketTestForm
{
    public partial class Form1 : Form
    {
        private ServerSocket server;
        private ClientSocket client;

        public Form1()
        {
            InitializeComponent();
            inputClient.KeyPress += new KeyPressEventHandler(HandleClientInput);
            inputServer.KeyPress += new KeyPressEventHandler(HandleServerInput);
            FormClosed += delegate (object sndr, FormClosedEventArgs evt)
            {
                if (server != null) server.Dispose();
            };
        }

        private void HandleServerInput(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return) return;
            string data = inputServer.Text;
            inputServer.Text = "";
            server?.SendAll(data, delegate (Exception ex)
            {
                ServerLog("Unable to write data to server");
            });
        }

        private void HandleClientInput(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return) return;
            string data = inputClient.Text;
            inputClient.Text = "";
            client?.Send(data, delegate (int bytes, Exception ex)
            {
                if (ex == null) ClientLog("Successful write of " + bytes + " bytes to server");
                else ClientLog("Unable to write data to server: " + ex.GetType().Name);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(StartServer)).Start();
        }

        private void StartServer()
        {
            server = new ServerSocket(12345, ServerLog);
            server.Bind();
        }

        private void ServerLog(string log)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(delegate { ServerLog(log); }));
            else
                outputServer.AppendText(log + Environment.NewLine);
        }

        private void ClientLog(string log)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(delegate { ClientLog(log); }));
            else
                outputClient.AppendText(log + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new ClientSocket(ClientLog);
            client.Connect("127.0.0.1", 12345);
        }
    }
}
