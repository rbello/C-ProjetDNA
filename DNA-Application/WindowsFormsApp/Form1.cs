﻿using GenomicAnalysis;
using NetworkComputeFramework.RunMode;
using System;
using System.Threading;
using System.Windows.Forms;
using NetworkComputeFramework.Node;
using NetworkComputeFramework.Worker;
using System.Data;
using GenomicAnalysis.Jobs;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {

        private IRunMode runMode;
        private GenomicAnalysisApplication app;

        public Form1()
        {
            InitializeComponent();
            app = new GenomicAnalysisApplication();
            connectionErrorText.Text = "";
            processingJobSelector.Items.AddRange(app.GetAvailableJobTypes());

            clusterNodesGrid.Columns.Add("nodeAddress", "Address");
            clusterNodesGrid.Columns.Add("nodeState", "State");
            clusterNodesGrid.Columns.Add("nodeWorkers", "Workers");
            clusterNodesGrid.Columns.Add("nodeCpuUsage", "CPU");
            clusterNodesGrid.Columns.Add("nodeMemoryUsage", "Memory");
            clusterNodesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            clusterNodesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            // Disable UI
            SetEnabledUI(false);
            // Start server mode
            AppendServerLog("Starting server on port:", serverPortSelector.Value);
            try
            {
                // Create server run mode
                runMode = new ServerMode<string, GenomicBase>(app);
                // Bind GUI on worker pool events
                runMode.OnStateChanged += OnWorkStateChanged;
                runMode.WorkerPool.OnNodeConnected += OnNodeConnected;
                runMode.WorkerPool.OnNodeDisconnected += OnNodeDisconnected;
                runMode.WorkerPool.OnWorkerPoolMessage += OnWorkerPoolMessage;
                // Start server
                runMode.Init(
                    // On success
                    delegate ()
                    {
                        AppendServerLog("Cluster is ready to handle connections");
                        // Let the data file and processing job choosable
                        SetControlEnabled(loadDataFileButton, true);
                        SetControlEnabled(processingJobSelector, true);
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
                SetEnabledUI(true);
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
            if (startStopProcessingButton.Text == "Stop") {
                AppendServerLog("Halting...");
                throw new NotImplementedException("TODO");
            }
            // Control input data
            if (processingJobSelector.SelectedIndex == -1)
            {
                AppendServerLog("Please select a job type");
                return;
            }
            // Disable UI
            processingJobSelector.Enabled = loadDataFileButton.Enabled = false;
            // Inverse button function
            startStopProcessingButton.Text = "Stop";
            // Load data and start processing distribution to cluster
            ((ServerMode<string, GenomicBase>)runMode).Start(
                // Success
                delegate ()
                {
                    AppendServerLog("Ok, everything is started !!");
                },
                // Failure
                delegate (Exception ex)
                {
                    AppendServerLog("Failure:", ex);
                },
                // Arguments
                OpenFileDialog.FileName,
                processingJobSelector.Text
            );
        }

        private void connectClusterButton_Click(object sender, EventArgs e)
        {
            SetEnabledUI(false);
        }

        private void SetEnabledUI(bool enabled)
        {
            // Server mode UI
            startServerButton.Enabled = serverPortSelector.Enabled = localThreadsCountSelector.Enabled = enabled;
            // Client mode UI
            connectClusterButton.Enabled = clusterAddressSelector.Enabled = clusterPortSelector.Enabled = enabled;
        }

        private void githubSourcesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/rbello/C-ProjetDNA");
        }

        private void AppendServerLog(params object[] message)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(delegate { AppendServerLog(message); }));
            else
                processingOutputLog.AppendText(string.Join(" ", message) + Environment.NewLine);
        }

        private void SetControlEnabled(Control ctrl, bool enabled)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(delegate { SetControlEnabled(ctrl, enabled); }));
            else
                ctrl.Enabled = enabled;
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
