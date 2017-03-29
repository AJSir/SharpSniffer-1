
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace SharpSniffer
{
    public partial class FormParserEditor : Form
    {
        public string ParserPathFileName;
        public ParserBase ParserCompiled;
        public FormParserEditor()
        {
            
            InitializeComponent();
            ParserPathFileName  = "";
            ParserCompiled      = null;
        }

        private void FormParserEditor_Load(object sender, EventArgs e)
        {
            UpdateTitle();
            LoadFile();
        }

        private bool m_bdirty=false;

        void UpdateTitle()
        {
            if (ParserPathFileName.Length == 0)
            {
                this.Text = "Parser Edit - New";
            }
            else
            {
                this.Text = "Parser Edit - " + ParserPathFileName;
            }

            if (m_bdirty)
            {
                this.Text = this.Text + " * ";
            }
        }

        void LoadFile()
        {
            if (ParserPathFileName.Length != 0 && File.Exists(ParserPathFileName))
            {
                FileStream fs = new FileStream(ParserPathFileName, FileMode.Open);
                byte[] data = new byte[fs.Length + 1];
                fs.Read(data, 0, (int)fs.Length);
                data[fs.Length] = 0;

                m_bdirty=false;
                TextBoxSource.Text = System.Text.Encoding.UTF8.GetString(data);
                fs.Close();
                fs.Dispose();
                
            }
            else
            {
                TextBoxSource.Text = SharpSniffer.Properties.Resources.ParserTempate;
            }
        }

        void savefile()
        {
            if (ParserPathFileName.Length == 0)
            {
                int lastIndex = Application.ExecutablePath.LastIndexOf('\\');
                String pathFilters = Application.ExecutablePath.Remove(lastIndex, Application.ExecutablePath.Length - lastIndex)
                    + "\\parser\\";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = pathFilters;
                dlg.Filter = "Filter file(*.cs)|*.cs";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ParserPathFileName = dlg.FileName;
                }
            }
            
            FileStream fs = new FileStream(ParserPathFileName, FileMode.Create);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(TextBoxSource.Text);
            fs.Write(data ,0,data.Length);
            fs.Close();
            fs.Dispose();
            m_bdirty = false;
            UpdateTitle();
            
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            savefile();
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            if (m_bdirty)
            {
                savefile();
            }


            ParserCompiled = CompileParserFromFile(ParserPathFileName);

       }

        public ParserBase CompileParserFromFile(String szfile)
        {
            if (szfile.Length <= 3 || !File.Exists(szfile))
                return null;

            int lastIndex = Application.ExecutablePath.LastIndexOf('\\');
            String ExePath = Application.ExecutablePath.Remove(lastIndex, Application.ExecutablePath.Length - lastIndex);
            System.Environment.CurrentDirectory = ExePath;

            CompilerParameters options = new CompilerParameters();
            options.ReferencedAssemblies.Add("SharpSniffer.exe");
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Drawing.dll");
            options.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            options.ReferencedAssemblies.Add("System.data.dll");
            options.ReferencedAssemblies.Add("System.Xml.dll");
            options.GenerateInMemory = true;
            options.GenerateExecutable = false;
            options.TreatWarningsAsErrors = false;


            CSharpCodeProvider pv = new CSharpCodeProvider();
            CompilerResults cr = pv.CompileAssemblyFromFile(options, szfile);

            TextBotOuput.SelectionColor = Color.Blue;
            TextBotOuput.AppendText("Compiling " + szfile + ".\r\n");

            TextBotOuput.SelectionColor = Color.Red;

            if (cr.Errors.Count > 0)
            {
                for (int i = 0; i < cr.Errors.Count; i++)
                {
                    TextBotOuput.AppendText(cr.Errors[i].ToString() + "\r\n");
                }
                return null;
            }

            Type t = null;
            Type[] ts = cr.CompiledAssembly.GetTypes();
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i].IsSubclassOf(typeof(ParserBase)))
                {
                    t = ts[i];
                    break;
                }
            }

            if (t == null)
            {
                TextBotOuput.AppendText("No class is sub class of ParserBase\r\n");
            }

            if (t.IsAbstract)
            {
                TextBotOuput.AppendText(t.Name + " is abstract\r\n");
                return null;
            }

            if (!t.IsPublic)
            {
                TextBotOuput.AppendText(t.Name + " is not Public\r\n");
                return null;
            }

            Object o = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null);
            if (o == null)
            {
                TextBotOuput.AppendText(t.Name + " CreateInstance failed\r\n");
                return null;
            }

            TextBotOuput.SelectionColor = Color.Blue;
            TextBotOuput.AppendText(t.Name + " Compiled\r\n");

            return (ParserBase)o;
        }

        private void TextBoxSource_TextChanged(object sender, EventArgs e)
        {
            if (!m_bdirty)
            {
                m_bdirty = true;
                UpdateTitle();
            }
        }

        private void FormParserEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bdirty)
            {
                DialogResult r = MessageBox.Show("Are you sure closing withou saving?",
                    "Editor",
                    MessageBoxButtons.OKCancel);

                if (r != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
