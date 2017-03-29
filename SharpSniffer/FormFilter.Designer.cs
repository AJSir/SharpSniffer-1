namespace SharpSniffer
{
    partial class FormFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUDP = new System.Windows.Forms.CheckBox();
            this.checkBoxTCP = new System.Windows.Forms.CheckBox();
            this.groupBoxIpAddress = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxIP = new System.Windows.Forms.ListBox();
            this.buttonIpRemove = new System.Windows.Forms.Button();
            this.buttonIpAdd = new System.Windows.Forms.Button();
            this.checkBoxIPExcluded = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.listBoxPort = new System.Windows.Forms.ListBox();
            this.checkBoxPortExcluded = new System.Windows.Forms.CheckBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonPortAdd = new System.Windows.Forms.Button();
            this.checkBoxOnlyData = new System.Windows.Forms.CheckBox();
            this.IDOK = new System.Windows.Forms.Button();
            this.IDCANCEL = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxIpAddress.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxUDP);
            this.groupBox1.Controls.Add(this.checkBoxTCP);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Protocol";
            // 
            // checkBoxUDP
            // 
            this.checkBoxUDP.AutoSize = true;
            this.checkBoxUDP.Location = new System.Drawing.Point(54, 20);
            this.checkBoxUDP.Name = "checkBoxUDP";
            this.checkBoxUDP.Size = new System.Drawing.Size(42, 16);
            this.checkBoxUDP.TabIndex = 1;
            this.checkBoxUDP.Text = "UDP";
            this.checkBoxUDP.UseVisualStyleBackColor = true;
            // 
            // checkBoxTCP
            // 
            this.checkBoxTCP.AutoSize = true;
            this.checkBoxTCP.Location = new System.Drawing.Point(6, 20);
            this.checkBoxTCP.Name = "checkBoxTCP";
            this.checkBoxTCP.Size = new System.Drawing.Size(42, 16);
            this.checkBoxTCP.TabIndex = 0;
            this.checkBoxTCP.Text = "TCP";
            this.checkBoxTCP.UseVisualStyleBackColor = true;
            // 
            // groupBoxIpAddress
            // 
            this.groupBoxIpAddress.Controls.Add(this.textBox1);
            this.groupBoxIpAddress.Controls.Add(this.label2);
            this.groupBoxIpAddress.Controls.Add(this.listBoxIP);
            this.groupBoxIpAddress.Controls.Add(this.buttonIpRemove);
            this.groupBoxIpAddress.Controls.Add(this.buttonIpAdd);
            this.groupBoxIpAddress.Controls.Add(this.checkBoxIPExcluded);
            this.groupBoxIpAddress.Controls.Add(this.label1);
            this.groupBoxIpAddress.Location = new System.Drawing.Point(3, 54);
            this.groupBoxIpAddress.Name = "groupBoxIpAddress";
            this.groupBoxIpAddress.Size = new System.Drawing.Size(426, 165);
            this.groupBoxIpAddress.TabIndex = 1;
            this.groupBoxIpAddress.TabStop = false;
            this.groupBoxIpAddress.Text = "IP";
            this.groupBoxIpAddress.Enter += new System.EventHandler(this.groupBoxIpAddress_Enter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(326, 21);
            this.textBox1.TabIndex = 10;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(335, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "PS: 192.168.0.2 is equal to 192.168.0.2/255.255.255.255";
            // 
            // listBoxIP
            // 
            this.listBoxIP.FormattingEnabled = true;
            this.listBoxIP.ItemHeight = 12;
            this.listBoxIP.Location = new System.Drawing.Point(6, 74);
            this.listBoxIP.Name = "listBoxIP";
            this.listBoxIP.Size = new System.Drawing.Size(326, 88);
            this.listBoxIP.TabIndex = 8;
            // 
            // buttonIpRemove
            // 
            this.buttonIpRemove.Location = new System.Drawing.Point(338, 74);
            this.buttonIpRemove.Name = "buttonIpRemove";
            this.buttonIpRemove.Size = new System.Drawing.Size(82, 23);
            this.buttonIpRemove.TabIndex = 7;
            this.buttonIpRemove.Text = "-remove";
            this.buttonIpRemove.UseVisualStyleBackColor = true;
            this.buttonIpRemove.Click += new System.EventHandler(this.buttonIpRemove_Click);
            // 
            // buttonIpAdd
            // 
            this.buttonIpAdd.Location = new System.Drawing.Point(338, 45);
            this.buttonIpAdd.Name = "buttonIpAdd";
            this.buttonIpAdd.Size = new System.Drawing.Size(82, 23);
            this.buttonIpAdd.TabIndex = 5;
            this.buttonIpAdd.Text = "+Add";
            this.buttonIpAdd.UseVisualStyleBackColor = true;
            this.buttonIpAdd.Click += new System.EventHandler(this.buttonIpAdd_Click);
            // 
            // checkBoxIPExcluded
            // 
            this.checkBoxIPExcluded.AutoSize = true;
            this.checkBoxIPExcluded.Location = new System.Drawing.Point(343, 131);
            this.checkBoxIPExcluded.Name = "checkBoxIPExcluded";
            this.checkBoxIPExcluded.Size = new System.Drawing.Size(72, 16);
            this.checkBoxIPExcluded.TabIndex = 4;
            this.checkBoxIPExcluded.Text = "Excluded";
            this.checkBoxIPExcluded.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP(format: ip/mask;example:192.168.0.1/255.255.255.0)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextBoxPort);
            this.groupBox2.Controls.Add(this.listBoxPort);
            this.groupBox2.Controls.Add(this.checkBoxPortExcluded);
            this.groupBox2.Controls.Add(this.buttonRemove);
            this.groupBox2.Controls.Add(this.buttonPortAdd);
            this.groupBox2.Location = new System.Drawing.Point(3, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 150);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port";
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Location = new System.Drawing.Point(190, 21);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(72, 21);
            this.TextBoxPort.TabIndex = 6;
            this.TextBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxPort_KeyPress);
            // 
            // listBoxPort
            // 
            this.listBoxPort.FormattingEnabled = true;
            this.listBoxPort.ItemHeight = 12;
            this.listBoxPort.Location = new System.Drawing.Point(9, 20);
            this.listBoxPort.Name = "listBoxPort";
            this.listBoxPort.Size = new System.Drawing.Size(172, 112);
            this.listBoxPort.TabIndex = 5;
            // 
            // checkBoxPortExcluded
            // 
            this.checkBoxPortExcluded.AutoSize = true;
            this.checkBoxPortExcluded.Location = new System.Drawing.Point(190, 68);
            this.checkBoxPortExcluded.Name = "checkBoxPortExcluded";
            this.checkBoxPortExcluded.Size = new System.Drawing.Size(72, 16);
            this.checkBoxPortExcluded.TabIndex = 4;
            this.checkBoxPortExcluded.Text = "Excluded";
            this.checkBoxPortExcluded.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(187, 119);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "-Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonPortAdd
            // 
            this.buttonPortAdd.Location = new System.Drawing.Point(187, 90);
            this.buttonPortAdd.Name = "buttonPortAdd";
            this.buttonPortAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonPortAdd.TabIndex = 1;
            this.buttonPortAdd.Text = "+Add";
            this.buttonPortAdd.UseVisualStyleBackColor = true;
            this.buttonPortAdd.Click += new System.EventHandler(this.buttonPortAdd_Click);
            // 
            // checkBoxOnlyData
            // 
            this.checkBoxOnlyData.AutoSize = true;
            this.checkBoxOnlyData.Location = new System.Drawing.Point(310, 360);
            this.checkBoxOnlyData.Name = "checkBoxOnlyData";
            this.checkBoxOnlyData.Size = new System.Drawing.Size(108, 16);
            this.checkBoxOnlyData.TabIndex = 6;
            this.checkBoxOnlyData.Text = "With data only";
            this.checkBoxOnlyData.UseVisualStyleBackColor = true;
            // 
            // IDOK
            // 
            this.IDOK.Location = new System.Drawing.Point(122, 396);
            this.IDOK.Name = "IDOK";
            this.IDOK.Size = new System.Drawing.Size(75, 23);
            this.IDOK.TabIndex = 7;
            this.IDOK.Text = "&OK";
            this.IDOK.UseVisualStyleBackColor = true;
            this.IDOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // IDCANCEL
            // 
            this.IDCANCEL.Location = new System.Drawing.Point(233, 396);
            this.IDCANCEL.Name = "IDCANCEL";
            this.IDCANCEL.Size = new System.Drawing.Size(75, 23);
            this.IDCANCEL.TabIndex = 8;
            this.IDCANCEL.Text = "&Cancel";
            this.IDCANCEL.UseVisualStyleBackColor = true;
            this.IDCANCEL.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 430);
            this.Controls.Add(this.IDCANCEL);
            this.Controls.Add(this.IDOK);
            this.Controls.Add(this.checkBoxOnlyData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxIpAddress);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormConfig";
            this.Text = "FormConfig";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxIpAddress.ResumeLayout(false);
            this.groupBoxIpAddress.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUDP;
        private System.Windows.Forms.CheckBox checkBoxTCP;
        private System.Windows.Forms.GroupBox groupBoxIpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonIpAdd;
        private System.Windows.Forms.CheckBox checkBoxIPExcluded;
        private System.Windows.Forms.Button buttonIpRemove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonPortAdd;
        private System.Windows.Forms.CheckBox checkBoxPortExcluded;
        private System.Windows.Forms.CheckBox checkBoxOnlyData;
        private System.Windows.Forms.Button IDOK;
        private System.Windows.Forms.Button IDCANCEL;
        private System.Windows.Forms.ListBox listBoxIP;
        private System.Windows.Forms.ListBox listBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TextBoxPort;
    }
}