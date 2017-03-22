using GenomicAnalysis;
using NetworkComputeFramework.RunMode;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {

        private IRunMode serverMode;
        private IRunMode clientMode;

        private GenomicAnalysisApplication app;

        public MainForm()
        {
            InitializeComponent();

            // Create application
            app = new GenomicAnalysisApplication();

            // Combo bow with availables process on data
            processSelector.Items.AddRange(app.GetAvailableProcessTypes());
            processSelector.SelectedIndex = 0;

            // Cluster nodes gride view
            clusterNodesGrid.Columns.Add("nodeAddress", "Node");
            clusterNodesGrid.Columns.Add("nodeState", "State");
            clusterNodesGrid.Columns.Add("nodeWorkers", "Workers");
            clusterNodesGrid.Columns.Add("nodeCpuUsage", "CPU");
            clusterNodesGrid.Columns.Add("nodeMemoryUsage", "Memory");
            clusterNodesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            clusterNodesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Adjust window position to center to screen
            CenterToScreen();
        }

        private void SetEnabledUI(bool clientServer, bool enabled)
        {
            // Server mode UI
            if (!clientServer) startServerButton.Enabled = serverPortSelector.Enabled = localThreadsCountSelector.Enabled = enabled;
            // Client mode UI
            if (clientServer)  connectClusterButton.Enabled = clusterAddressSelector.Enabled = clusterPortSelector.Enabled = enabled;
        }

        private void githubSourcesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/rbello/C-ProjetDNA");
        }

        private void Log(string message)
        {
            AppendServerLog(message);
        }

        private void AppendServerLog(params object[] message)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new ThreadStart(delegate { AppendServerLog(message); }));
                else
                    processingOutputLog.AppendText(string.Join(" ", message) + Environment.NewLine);
            }
            catch
            {
                return;
            }
        }

        private void SetControlEnabled(Control ctrl, bool enabled)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(delegate { SetControlEnabled(ctrl, enabled); }));
            else
                ctrl.Enabled = enabled;
        }

    }
}
