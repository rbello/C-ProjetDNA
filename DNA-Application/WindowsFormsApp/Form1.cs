using NetworkComputeFramework;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {

        private IRunMode runMode;

        public Form1()
        {
            InitializeComponent();
            connectionErrorText.Text = "";
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            // Disable UI
            SetEnabledUI(false);
            // Start server mode
            AppendServerLog("Starting server on port:", serverPortSelector.Value);
            runMode = new ServerMode();
            try
            {
                runMode.Start(
                    // On success
                    delegate ()
                    {
                        AppendServerLog("Server is listenning...");
                        // Let the data file and processing job choosable
                        SetControlEnabled(loadDataFileButton, true);
                        SetControlEnabled(processingJobSelector, true);
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
            ((ServerMode)runMode).LoadAndStartProcessingFile(OpenFileDialog.FileName, processingJobSelector.Text,
                // Success
                delegate ()
                {
                    AppendServerLog("Ok, everything is ready !!");
                },
                // Failure
                delegate (Exception ex)
                {
                    AppendServerLog("Failure:", ex);
                });
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

    }
}
