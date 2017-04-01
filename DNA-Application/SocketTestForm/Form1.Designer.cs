namespace SocketTestForm
{
    partial class Form1
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
            this.outputServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputClient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClient = new System.Windows.Forms.Button();
            this.buttonServer = new System.Windows.Forms.Button();
            this.inputServer = new System.Windows.Forms.TextBox();
            this.inputClient = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // outputServer
            // 
            this.outputServer.Location = new System.Drawing.Point(13, 36);
            this.outputServer.Multiline = true;
            this.outputServer.Name = "outputServer";
            this.outputServer.Size = new System.Drawing.Size(301, 211);
            this.outputServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server";
            // 
            // outputClient
            // 
            this.outputClient.Location = new System.Drawing.Point(320, 36);
            this.outputClient.Multiline = true;
            this.outputClient.Name = "outputClient";
            this.outputClient.Size = new System.Drawing.Size(324, 211);
            this.outputClient.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(430, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Clients";
            // 
            // buttonClient
            // 
            this.buttonClient.Location = new System.Drawing.Point(569, 7);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(75, 23);
            this.buttonClient.TabIndex = 4;
            this.buttonClient.Text = "Connect";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonServer
            // 
            this.buttonServer.Location = new System.Drawing.Point(236, 8);
            this.buttonServer.Name = "buttonServer";
            this.buttonServer.Size = new System.Drawing.Size(75, 23);
            this.buttonServer.TabIndex = 5;
            this.buttonServer.Text = "Open";
            this.buttonServer.UseVisualStyleBackColor = true;
            this.buttonServer.Click += new System.EventHandler(this.button2_Click);
            // 
            // inputServer
            // 
            this.inputServer.Location = new System.Drawing.Point(13, 254);
            this.inputServer.Name = "inputServer";
            this.inputServer.Size = new System.Drawing.Size(301, 20);
            this.inputServer.TabIndex = 6;
            // 
            // inputClient
            // 
            this.inputClient.Location = new System.Drawing.Point(320, 253);
            this.inputClient.Name = "inputClient";
            this.inputClient.Size = new System.Drawing.Size(324, 20);
            this.inputClient.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 287);
            this.Controls.Add(this.inputClient);
            this.Controls.Add(this.inputServer);
            this.Controls.Add(this.buttonServer);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outputClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClient;
        private System.Windows.Forms.Button buttonServer;
        private System.Windows.Forms.TextBox inputServer;
        private System.Windows.Forms.TextBox inputClient;
    }
}

