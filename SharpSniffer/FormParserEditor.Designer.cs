namespace SharpSniffer
{
    partial class FormParserEditor
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonBuild = new System.Windows.Forms.ToolStripButton();
            this.ButtonSave = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TextBoxSource = new System.Windows.Forms.TextBox();
            this.TextBotOuput = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonBuild,
            this.ButtonSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(640, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonBuild
            // 
            this.buttonBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonBuild.Image = global::SharpSniffer.Properties.Resources.build;
            this.buttonBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(23, 22);
            this.buttonBuild.Text = "Build";
            this.buttonBuild.ToolTipText = "Build";
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonSave.Image = global::SharpSniffer.Properties.Resources.save;
            this.ButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(23, 22);
            this.ButtonSave.Text = "Save";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TextBoxSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TextBotOuput);
            this.splitContainer1.Size = new System.Drawing.Size(640, 511);
            this.splitContainer1.SplitterDistance = 399;
            this.splitContainer1.TabIndex = 1;
            // 
            // TextBoxSource
            // 
            this.TextBoxSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxSource.Location = new System.Drawing.Point(0, 0);
            this.TextBoxSource.Multiline = true;
            this.TextBoxSource.Name = "TextBoxSource";
            this.TextBoxSource.Size = new System.Drawing.Size(640, 399);
            this.TextBoxSource.TabIndex = 0;
            this.TextBoxSource.WordWrap = false;
            this.TextBoxSource.TextChanged += new System.EventHandler(this.TextBoxSource_TextChanged);
            // 
            // TextBotOuput
            // 
            this.TextBotOuput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBotOuput.Location = new System.Drawing.Point(0, 0);
            this.TextBotOuput.Name = "TextBotOuput";
            this.TextBotOuput.Size = new System.Drawing.Size(640, 108);
            this.TextBotOuput.TabIndex = 0;
            this.TextBotOuput.Text = "";
            this.TextBotOuput.WordWrap = false;
            // 
            // FormParserEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 536);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormParserEditor";
            this.Text = "FormParserEditor";
            this.Load += new System.EventHandler(this.FormParserEditor_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormParserEditor_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox TextBotOuput;
        private System.Windows.Forms.ToolStripButton buttonBuild;
        private System.Windows.Forms.ToolStripButton ButtonSave;
        private System.Windows.Forms.TextBox TextBoxSource;
    }
}