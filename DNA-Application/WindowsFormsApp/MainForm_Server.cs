using GenomicAnalysis;
using NetworkComputeFramework.RunMode;
using System;
using System.Windows.Forms;
using System.Threading;
using NetworkComputeFramework.Worker;
using NetworkComputeFramework.Node;

namespace WindowsFormsApp
{
    public partial class MainForm
    {

        private void startServerButton_Click(object sender, EventArgs e)
        {
            // Disable UI
            SetEnabledUI(false, false);
            // Start server mode
            try
            {
                // Create server run mode
                serverMode = new ServerMode<string, GenomicNucleotidePeer>(app, this.Log);
                // Bind GUI on worker pool events
                serverMode.OnStateChanged += OnWorkStateChanged;
                serverMode.WorkerPool.OnNodeConnected += OnNodeConnected;
                serverMode.WorkerPool.OnNodeDisconnected += OnNodeDisconnected;
                serverMode.WorkerPool.OnWorkerPoolMessage += OnWorkerPoolMessage;
                // Bind execution mode stop function to window closing
                FormClosed += delegate (object sndr, FormClosedEventArgs evt)
                {
                    serverMode.Stop();
                };
                // Start server
                serverMode.Init(
                    // On success
                    delegate ()
                    {
                        AppendServerLog("Cluster is ready to accept connections...");
                        // Let the data file and process choosable
                        SetControlEnabled(loadDataFileButton, true);
                        SetControlEnabled(processSelector, true);
                        // Enable timer
                        Invoke(new ThreadStart(delegate {
                            clusterGridUpdateTimer.Enabled = true;
                        }));
                    },
                    // On failure
                    delegate (Exception ex)
                    {
                        AppendServerLog("Error starting mode :", ex);
                    },
                    // Arguments
                    serverPortSelector.Value,
                    localThreadsCountSelector.Value
                );
            }
            catch (Exception ex)
            {
                AppendServerLog("Fatal error :", ex);
                SetEnabledUI(false, true);
            }
        }

        private void OnWorkStateChanged(RunState newState)
        {
            AppendServerLog("New state:", newState);
        }

        private void OnWorkerPoolMessage(string message, LogLevel type)
        {
            AppendServerLog("[Pool]", message);
        }

        private void OnNodeConnected(INode node)
        {
            AppendServerLog("Node connected:", node);
            Invoke(new ThreadStart(delegate {
                clusterNodesGrid.Rows.Add(
                    node, "Idle", "0/" + node.Workers.Count, node.CpuUsage + "%", node.MemoryUsage + "MB"
                );
            }));
        }

        private void OnNodeDisconnected(INode node)
        {
            AppendServerLog("Node disconnected:", node);
        }

        private void loadDataFileButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Log
                AppendServerLog("DNA file:", OpenFileDialog.FileName);
                // Processing could be started
                startStopProcessingButton.Enabled = true;
            }
        }

        private void startStopProcessingButton_Click(object sender, EventArgs e)
        {
            // Stop operations
            if (startStopProcessingButton.Text == "Stop")
            {
                AppendServerLog("Halting...");
                serverMode.Stop();
                //TODO: Swap UI to initial state
                return;
            }
            // Control input data
            if (processSelector.SelectedIndex == -1)
            {
                AppendServerLog("Please select a process type");
                return;
            }
            // Disable UI
            processSelector.Enabled = loadDataFileButton.Enabled = false;
            // Inverse button function
            startStopProcessingButton.Text = "Stop";
            // Load data and start processing distribution to cluster
            ((ServerMode<string, GenomicNucleotidePeer>)serverMode).Start(
                // Success
                delegate (object finalResult)
                {
                    AppendServerLog("PROCESS FINISHED !");
                    AppendServerLog(finalResult);
                },
                // Failure
                delegate (Exception ex)
                {
                    AppendServerLog("PROCESS FAILURE : ", ex.GetType().Name, ex.Message);
                },
                // Arguments
                OpenFileDialog.FileName,
                processSelector.Text
            );
        }

        private void clusterGridUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update datagrid view with fresh data
            foreach (DataGridViewRow row in clusterNodesGrid.Rows)
            {
                INode node = (INode)row.Cells[0].Value;
                row.SetValues(node, node.Active ? "Process" : "Idle",
                    node.ActiveWorkersCount + "/" + node.Workers.Count, Math.Round(node.CpuUsage, 2) + "%",
                    node.MemoryUsage + "MB");
            }
        }

    }
}
