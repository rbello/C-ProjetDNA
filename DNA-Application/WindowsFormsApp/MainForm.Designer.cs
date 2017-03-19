namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clusterNodesGrid = new System.Windows.Forms.DataGridView();
            this.serverPortSelector = new System.Windows.Forms.NumericUpDown();
            this.localThreadsCountSelector = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.processSelector = new System.Windows.Forms.ComboBox();
            this.processingOutputLog = new System.Windows.Forms.TextBox();
            this.processingProgressBar = new System.Windows.Forms.ProgressBar();
            this.startStopProcessingButton = new System.Windows.Forms.Button();
            this.loadDataFileButton = new System.Windows.Forms.Button();
            this.startServerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.connectionErrorText = new System.Windows.Forms.Label();
            this.connectionStateLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clusterLogText = new System.Windows.Forms.TextBox();
            this.connectClusterButton = new System.Windows.Forms.Button();
            this.clusterAddressSelector = new System.Windows.Forms.TextBox();
            this.clusterPortSelector = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.githubSourcesLink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.clusterGridUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clusterNodesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPortSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localThreadsCountSelector)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clusterPortSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(501, 502);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clusterNodesGrid);
            this.tabPage1.Controls.Add(this.serverPortSelector);
            this.tabPage1.Controls.Add(this.localThreadsCountSelector);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.startServerButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(493, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server mode";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // clusterNodesGrid
            // 
            this.clusterNodesGrid.AllowUserToAddRows = false;
            this.clusterNodesGrid.AllowUserToDeleteRows = false;
            this.clusterNodesGrid.AllowUserToOrderColumns = true;
            this.clusterNodesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clusterNodesGrid.Location = new System.Drawing.Point(19, 37);
            this.clusterNodesGrid.Name = "clusterNodesGrid";
            this.clusterNodesGrid.ReadOnly = true;
            this.clusterNodesGrid.RowHeadersVisible = false;
            this.clusterNodesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clusterNodesGrid.Size = new System.Drawing.Size(468, 131);
            this.clusterNodesGrid.TabIndex = 9;
            // 
            // serverPortSelector
            // 
            this.serverPortSelector.Location = new System.Drawing.Point(55, 10);
            this.serverPortSelector.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.serverPortSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.serverPortSelector.Name = "serverPortSelector";
            this.serverPortSelector.Size = new System.Drawing.Size(61, 20);
            this.serverPortSelector.TabIndex = 8;
            this.serverPortSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.serverPortSelector.Value = new decimal(new int[] {
            6001,
            0,
            0,
            0});
            // 
            // localThreadsCountSelector
            // 
            this.localThreadsCountSelector.Location = new System.Drawing.Point(299, 10);
            this.localThreadsCountSelector.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.localThreadsCountSelector.Name = "localThreadsCountSelector";
            this.localThreadsCountSelector.Size = new System.Drawing.Size(68, 20);
            this.localThreadsCountSelector.TabIndex = 7;
            this.localThreadsCountSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.localThreadsCountSelector.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Local threads:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.processSelector);
            this.groupBox1.Controls.Add(this.processingOutputLog);
            this.groupBox1.Controls.Add(this.processingProgressBar);
            this.groupBox1.Controls.Add(this.startStopProcessingButton);
            this.groupBox1.Controls.Add(this.loadDataFileButton);
            this.groupBox1.Location = new System.Drawing.Point(19, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 296);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data processing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Process to execute :";
            // 
            // processSelector
            // 
            this.processSelector.Enabled = false;
            this.processSelector.FormattingEnabled = true;
            this.processSelector.Location = new System.Drawing.Point(223, 19);
            this.processSelector.Name = "processSelector";
            this.processSelector.Size = new System.Drawing.Size(171, 21);
            this.processSelector.TabIndex = 5;
            // 
            // processingOutputLog
            // 
            this.processingOutputLog.Location = new System.Drawing.Point(6, 65);
            this.processingOutputLog.Multiline = true;
            this.processingOutputLog.Name = "processingOutputLog";
            this.processingOutputLog.ReadOnly = true;
            this.processingOutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.processingOutputLog.Size = new System.Drawing.Size(456, 225);
            this.processingOutputLog.TabIndex = 4;
            // 
            // processingProgressBar
            // 
            this.processingProgressBar.Location = new System.Drawing.Point(6, 46);
            this.processingProgressBar.Name = "processingProgressBar";
            this.processingProgressBar.Size = new System.Drawing.Size(456, 13);
            this.processingProgressBar.TabIndex = 3;
            // 
            // startStopProcessingButton
            // 
            this.startStopProcessingButton.Enabled = false;
            this.startStopProcessingButton.Location = new System.Drawing.Point(400, 19);
            this.startStopProcessingButton.Name = "startStopProcessingButton";
            this.startStopProcessingButton.Size = new System.Drawing.Size(62, 23);
            this.startStopProcessingButton.TabIndex = 1;
            this.startStopProcessingButton.Text = "Start";
            this.startStopProcessingButton.UseVisualStyleBackColor = true;
            this.startStopProcessingButton.Click += new System.EventHandler(this.startStopProcessingButton_Click);
            // 
            // loadDataFileButton
            // 
            this.loadDataFileButton.Enabled = false;
            this.loadDataFileButton.Location = new System.Drawing.Point(6, 19);
            this.loadDataFileButton.Name = "loadDataFileButton";
            this.loadDataFileButton.Size = new System.Drawing.Size(91, 23);
            this.loadDataFileButton.TabIndex = 0;
            this.loadDataFileButton.Text = "Load DNA file";
            this.loadDataFileButton.UseVisualStyleBackColor = true;
            this.loadDataFileButton.Click += new System.EventHandler(this.loadDataFileButton_Click);
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(122, 8);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(90, 23);
            this.startServerButton.TabIndex = 3;
            this.startServerButton.Text = "Start server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.connectionErrorText);
            this.tabPage2.Controls.Add(this.connectionStateLabel);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.connectClusterButton);
            this.tabPage2.Controls.Add(this.clusterAddressSelector);
            this.tabPage2.Controls.Add(this.clusterPortSelector);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(493, 476);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Client mode";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // connectionErrorText
            // 
            this.connectionErrorText.AutoSize = true;
            this.connectionErrorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionErrorText.Location = new System.Drawing.Point(73, 90);
            this.connectionErrorText.Name = "connectionErrorText";
            this.connectionErrorText.Size = new System.Drawing.Size(0, 13);
            this.connectionErrorText.TabIndex = 8;
            // 
            // connectionStateLabel
            // 
            this.connectionStateLabel.AutoSize = true;
            this.connectionStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStateLabel.ForeColor = System.Drawing.Color.Red;
            this.connectionStateLabel.Location = new System.Drawing.Point(70, 73);
            this.connectionStateLabel.Name = "connectionStateLabel";
            this.connectionStateLabel.Size = new System.Drawing.Size(44, 13);
            this.connectionStateLabel.TabIndex = 7;
            this.connectionStateLabel.Text = "Offline";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Status :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clusterLogText);
            this.groupBox2.Location = new System.Drawing.Point(20, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 365);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // clusterLogText
            // 
            this.clusterLogText.Location = new System.Drawing.Point(6, 19);
            this.clusterLogText.Multiline = true;
            this.clusterLogText.Name = "clusterLogText";
            this.clusterLogText.ReadOnly = true;
            this.clusterLogText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clusterLogText.Size = new System.Drawing.Size(455, 340);
            this.clusterLogText.TabIndex = 0;
            // 
            // connectClusterButton
            // 
            this.connectClusterButton.Location = new System.Drawing.Point(293, 11);
            this.connectClusterButton.Name = "connectClusterButton";
            this.connectClusterButton.Size = new System.Drawing.Size(75, 45);
            this.connectClusterButton.TabIndex = 4;
            this.connectClusterButton.Text = "Connect";
            this.connectClusterButton.UseVisualStyleBackColor = true;
            this.connectClusterButton.Click += new System.EventHandler(this.connectClusterButton_Click);
            // 
            // clusterAddressSelector
            // 
            this.clusterAddressSelector.Location = new System.Drawing.Point(126, 13);
            this.clusterAddressSelector.Name = "clusterAddressSelector";
            this.clusterAddressSelector.Size = new System.Drawing.Size(161, 20);
            this.clusterAddressSelector.TabIndex = 3;
            // 
            // clusterPortSelector
            // 
            this.clusterPortSelector.Location = new System.Drawing.Point(126, 39);
            this.clusterPortSelector.Maximum = new decimal(new int[] {
            25535,
            0,
            0,
            0});
            this.clusterPortSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clusterPortSelector.Name = "clusterPortSelector";
            this.clusterPortSelector.Size = new System.Drawing.Size(71, 20);
            this.clusterPortSelector.TabIndex = 2;
            this.clusterPortSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clusterPortSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cluster port :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cluster address :";
            // 
            // githubSourcesLink
            // 
            this.githubSourcesLink.AutoSize = true;
            this.githubSourcesLink.Location = new System.Drawing.Point(531, 492);
            this.githubSourcesLink.Name = "githubSourcesLink";
            this.githubSourcesLink.Size = new System.Drawing.Size(133, 13);
            this.githubSourcesLink.TabIndex = 2;
            this.githubSourcesLink.TabStop = true;
            this.githubSourcesLink.Text = "By R.BELLO - Get sources";
            this.githubSourcesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubSourcesLink_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "For education purpose only";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WindowsFormsApp.Properties.Resources.LogoDNA;
            this.pictureBox1.Location = new System.Drawing.Point(519, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(156, 268);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // clusterGridUpdateTimer
            // 
            this.clusterGridUpdateTimer.Interval = 1000;
            this.clusterGridUpdateTimer.Tick += new System.EventHandler(this.clusterGridUpdateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 526);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.githubSourcesLink);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "C-DNA - Distributed processing application";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clusterNodesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPortSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localThreadsCountSelector)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clusterPortSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox processingOutputLog;
        private System.Windows.Forms.ProgressBar processingProgressBar;
        private System.Windows.Forms.Button startStopProcessingButton;
        private System.Windows.Forms.Button loadDataFileButton;
        private System.Windows.Forms.NumericUpDown localThreadsCountSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown serverPortSelector;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel githubSourcesLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button connectClusterButton;
        private System.Windows.Forms.TextBox clusterAddressSelector;
        private System.Windows.Forms.NumericUpDown clusterPortSelector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label connectionStateLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox clusterLogText;
        private System.Windows.Forms.Label connectionErrorText;
        private System.Windows.Forms.ComboBox processSelector;
        private System.Windows.Forms.DataGridView clusterNodesGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer clusterGridUpdateTimer;
    }
}

