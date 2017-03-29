using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace SharpSniffer
{
    public partial class FormMain : Form
    {
        private Socket m_socket = null;
        private SnifferFilter m_filter=null;
        private int m_filterCurIndex;
        private int m_ParserCurIndex;
        private byte[] m_buffer = new byte[65535];
        private ParserBase m_parser=null;
        private IPAddress m_sniffingIPAddress=null;
        private string m_szFiltersPath;
        private string m_szParsersPath;
        
        public FormMain()
        {
            InitializeComponent();


            LoadInterfaces();
            LoadFilters();
            LoadParser();

            

        }

        void LoadInterfaces()
        {
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < entry.AddressList.Length; i++)
            {
                ComboBoxInterface.Items.Add(entry.AddressList[i]);
            }

            if (entry.AddressList.Length > 0)
            {
                ComboBoxInterface.SelectedIndex = 0;
            }
        }

        void LoadParser()
        {
            int lastIndex = Application.ExecutablePath.LastIndexOf('\\');
            m_szParsersPath = Application.ExecutablePath.Remove(lastIndex, Application.ExecutablePath.Length - lastIndex)
                + "\\Parser\\";
            String[] ssf = Directory.GetFiles(m_szParsersPath,"*.cs");
            toolStripComboBoxParser.Items.Add(new SharpSnifferParser("", "Builtin", new ParserBase()));
            for (int i = 0; ssf != null && i < ssf.Length; i++)
            {
                ParserBase parser = CompileParserFromFile(ssf[i]);
                toolStripComboBoxParser.Items.Add(new SharpSnifferParser(ssf[i],
                    Path.GetFileName(ssf[i]), parser));
                
            }
            toolStripComboBoxParser.Items.Add("New...");

            toolStripComboBoxParser.SelectedIndex = 0;
            m_ParserCurIndex                      = 0;
            toolStripButtonCompile.Enabled = false;
            toolStripButtonEditParser.Enabled = false;

            m_parser = ((SharpSnifferParser)toolStripComboBoxParser.SelectedItem).Parser;
        }

        void LoadFilters()
        {
            int lastIndex = Application.ExecutablePath.LastIndexOf('\\');
            m_szFiltersPath = Application.ExecutablePath.Remove(lastIndex, Application.ExecutablePath.Length - lastIndex)
                + "\\filter\\";
            String[] ssf = Directory.GetFiles(m_szFiltersPath, "*.flt");
            comboBoxFilters.Items.Add("NONE");
            for(int i = 0; ssf != null && i < ssf.Length; i++)
            {
                SnifferFilter filter = new SnifferFilter();
                filter.Load(ssf[i]);
                comboBoxFilters.Items.Add(filter);
            }
            comboBoxFilters.Items.Add("New...");
            comboBoxFilters.SelectedIndex = 0;
            toolStripButtonFilter.Enabled = false;
            m_filterCurIndex = 0;
        }

        void TestCloseWait()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress addr = IPAddress.Parse("10.0.0.5");
            s.Connect(new IPEndPoint(addr, 10002));
            byte [] data = {25};
            s.Close();
            //s.Send(data);
            
        }

        void testhttpclient()
        {
            string host = "www.sina.com.cn";
            IPHostEntry entry = Dns.GetHostEntry(host);
            if (entry.AddressList.Length < 1)
                return;

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress addr = entry.AddressList[0];
            s.Connect(new IPEndPoint(addr, 80));
            String srequest = "GET / HTTP/1.1\r\nHost: " + host + "\r\nConnection: Close\r\n\r\n";

            Byte[] bytesSent = Encoding.ASCII.GetBytes(srequest);

            string str = "";
            s.Send(bytesSent, SocketFlags.None);
            byte[] buf = new byte[1024];
            int bytes = 0;

            do
            {
                bytes = s.Receive(buf, 0);
                str += Encoding.Default.GetString(buf, 0, bytes);
            }
            while (bytes > 0);

            richTextBox1.AppendText(Encoding.Default.ToString());
            richTextBox1.AppendText(str);

            s.Close();
        }

        void testsniffer()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress addr = null;
            for(int i=0;i<entry.AddressList.Length;i++)
            {

                if (entry.AddressList[i].ToString().Contains("10.0."))
                {
                    addr = entry.AddressList[i];
                    break;
                }
            }
            if(addr==null)
            {
                return;
            }

            s.Bind(new IPEndPoint(addr, 12345));
            byte [] inctrl={1,0,0,0};
            byte [] outctrl={1,0,0,0};
            int ret =  s.IOControl(IOControlCode.ReceiveAll, inctrl, outctrl);
            byte[] buf = new byte[65535];
            for(int i=0;i<10;i++)
            {
                ret = s.Receive(buf,buf.Length, SocketFlags.None);
                // Creates an IPEndPoint to capture the identity of the sending host.
                //IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                //EndPoint senderRemote = (EndPoint)sender;
                //ret = s.ReceiveFrom(buf, ref senderRemote);
                //
                if (ret > 0)
                {
                    richTextBox1.AppendText("receiveed" + i);
                    //richTextBox1.AppendText(senderRemote.ToString()+ ret + "/r/n");
                }
            }

            s.Close();
        }

       
        void StartSniffer()
        {
            if (m_socket == null)
            {
                if (ComboBoxInterface.SelectedItem != null)
                {
                    IPAddress addr = (IPAddress)ComboBoxInterface.SelectedItem;
                    m_sniffingIPAddress = addr;
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                    s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                    s.Bind(new IPEndPoint(addr, 0));
                    m_socket = s;

                    byte[] inctrl = { 1, 0, 0, 0 };
                    byte[] outctrl = { 0, 0, 0, 0 };
                    int ret = m_socket.IOControl(IOControlCode.ReceiveAll, inctrl, outctrl);
                    if (ret != 0)
                    {
                        MessageBox.Show("IOControl 失败");
                        StopSniffer();
                    }

                    s.BeginReceive(m_buffer, 0, m_buffer.Length, 0, new AsyncCallback(OnPacket), null);
                    toolStripButtonStartPause.Image = SharpSniffer.Properties.Resources.pause;
                    toolStripButtonStartPause.ToolTipText = "停止";
                    
                    ComboBoxInterface.Enabled = false;
                    comboBoxFilters.Enabled = false;
                    toolStripComboBoxParser.Enabled = false;

                    toolStripButtonFilter.Enabled = false;
                    toolStripButtonCompile.Enabled = false;
                    toolStripButtonEditParser.Enabled = false;


                    
                }
                else
                {
                    MessageBox.Show("必须选择一个网络接口");
                }
            }

        }

        void StopSniffer()
        {
            if (m_socket != null)
            {
                m_socket.Shutdown(SocketShutdown.Both);
                m_socket.Close();
                m_socket = null;
            
                comboBoxFilters.Enabled     = true;
                ComboBoxInterface.Enabled   = true;
                toolStripComboBoxParser.Enabled = true;
                
                toolStripButtonFilter.Enabled = true;


                if (toolStripComboBoxParser.SelectedIndex <= 0 &&
                 toolStripComboBoxParser.SelectedIndex == (toolStripComboBoxParser.Items.Count - 1))
                {
                    toolStripButtonCompile.Enabled = false;
                    toolStripButtonEditParser.Enabled = false;
                }
                else
                {
                    toolStripButtonCompile.Enabled = true;
                    toolStripButtonEditParser.Enabled = true;
                }

                if (comboBoxFilters.SelectedIndex != 0 
                    && comboBoxFilters.SelectedIndex != (comboBoxFilters.Items.Count - 1))
                {
                    toolStripButtonFilter.Enabled = true;
                    
                }
                else
                {
                    toolStripButtonFilter.Enabled = false;
                    
                }


            }
            toolStripButtonStartPause.Image = SharpSniffer.Properties.Resources.run;
            toolStripButtonStartPause.ToolTipText = "开始";
        }

        delegate void AppendMessageCallback(string text);
        private void AppendMessage(string text)
        {
            richTextBox1.AppendText(text);
        }


        bool CheckWithFilter(IPHeader ipHdr, TCPHeader tcpHdr, UDPHeader udpHdr)
        {
            if (m_filter == null)
                return true;

       
            if (!m_filter.IncludedProtocol(ipHdr.Protocol))
            {
                return false;
            }

            if (m_filter.AddressList.Count > 0)
            {
                IPAddress remoteAddr1 = null;
                IPAddress remoteAddr2 = null;
                if (ipHdr.DestinationAddress.Address == m_sniffingIPAddress.Address)
                {
                    remoteAddr1 = ipHdr.SourceAddress;
                }
                else if (ipHdr.SourceAddress.Address == m_sniffingIPAddress.Address)
                {
                    remoteAddr1 = ipHdr.DestinationAddress;
                }
                else
                {
                    remoteAddr1 = ipHdr.SourceAddress;
                    remoteAddr2 = ipHdr.DestinationAddress;
                }

                if (remoteAddr2 == null)
                {
                    if ((m_filter.AddressExcluded == m_filter.IncludedAddress(remoteAddr1)))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!m_filter.AddressExcluded)
                    {
                        if(!m_filter.IncludedAddress(remoteAddr1)
                            &&!m_filter.IncludedAddress(remoteAddr2))
                        {
                            return false;
                                
                        }
                    }
                    else
                    {
                        if(m_filter.IncludedAddress(remoteAddr1)
                            ||m_filter.IncludedAddress(remoteAddr2))
                        {
                            return false;
                                
                        }

                    }
                }
            }

            ushort destport = 0;
            ushort srcport = 0;
            ushort msglen = 0;

            if (tcpHdr != null )
            {
                destport    = tcpHdr.DestinationPort;
                srcport     = tcpHdr.SourcePort;
                msglen = tcpHdr.MessageLength;
            }
            else if (udpHdr != null)
            {
                destport = udpHdr.DestinationPort;
                srcport = udpHdr.SourcePort;
                msglen = udpHdr.MessageLength;
            }
            else
            {
                return false;
            }

            if (m_filter.DataOnly && msglen <= 0)
            {
                return false;
            }

            if (m_filter.Portlist.Count > 0)
            {
                if (!m_filter.PortExluded)
                {
                    if ((!m_filter.IncludedPort(destport)
                        && !m_filter.IncludedPort(srcport))
                        )
                    {
                        return false;
                    }
                }
                else
                {
                    if ((m_filter.IncludedPort(destport)
                        || m_filter.IncludedPort(srcport))
                        )
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        delegate void OnPacketCallback(IAsyncResult ars);
        private void OnPacketSync(IAsyncResult ars)
        {
            if (m_socket == null) return;
            Socket s = m_socket;
            int nbytes = s.EndReceive(ars);
            if (nbytes > 0)
            {
                IPHeader ipHdr = new IPHeader(m_buffer, nbytes);
                TCPHeader tcpHdr = null;
                UDPHeader udpHdr = null;

                if (ipHdr.Protocol == ProtocolType.Tcp)
                {
                    tcpHdr = new TCPHeader(ipHdr.Data, ipHdr.MessageLength);

                }
                else if (ipHdr.Protocol == ProtocolType.Udp)
                {
                    udpHdr = new UDPHeader(ipHdr.Data, ipHdr.MessageLength);

                }
                else
                {
                    //todo
                }

                if (CheckWithFilter(ipHdr, tcpHdr, udpHdr))
                {
                    if (m_parser != null)
                    {
                        m_parser.OnPacket(richTextBox1, ipHdr, tcpHdr, udpHdr);
                    }
                }

                s.BeginReceive(m_buffer, 0, m_buffer.Length, 0, new AsyncCallback(OnPacket), null);
            }
            else
            {
                richTextBox1.AppendText("Socket出错");
            } 

        }

        void OnPacket(IAsyncResult ars)
        {
            richTextBox1.Invoke(new OnPacketCallback(OnPacketSync),new object[]{ars});
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //TestCloseWait();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            StopSniffer();
        }

        private void toolStripButtonStartPause_Click(object sender, EventArgs e)
        {
            if (m_socket == null)
            {
                StartSniffer();
                   
            }
            else
            {
                StopSniffer();
                
            }
        }

        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {
            if (m_socket == null && m_filter!=null)
            {
                FormFilter formConfig = new FormFilter();
                formConfig.SetFilter(m_filter);
                DialogResult dr = formConfig.ShowDialog(this);

            }
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            AboutBoxDany aboutbox = new AboutBoxDany();
            aboutbox.ShowDialog();
        }

        

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopSniffer();
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

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.AppendText("Compiling " + szfile + ".\r\n");

            richTextBox1.SelectionColor = Color.Red;
           
            if (cr.Errors.Count > 0)
            {
                for (int i = 0; i < cr.Errors.Count; i++)
                {
                    richTextBox1.AppendText(cr.Errors[i].ToString()+"\r\n");
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
                richTextBox1.AppendText("No class is sub class of ParserBase\r\n");
                return null;
            }

            if (t.IsAbstract)
            {
                richTextBox1.AppendText(t.Name+" is abstract\r\n");
                return null;
            }

            if (!t.IsPublic)
            {
                richTextBox1.AppendText(t.Name + " is not Public\r\n");
                return null;
            }

            Object o = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null);
            if (o == null)
            {
                richTextBox1.AppendText(t.Name + " CreateInstance failed\r\n");
                return null;
            }

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.AppendText(t.Name + " Compiled\r\n");

            return (ParserBase)o;
        }

        private void comboBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFilters.SelectedIndex != 0 && comboBoxFilters.SelectedIndex != (comboBoxFilters.Items.Count - 1))
            {
                toolStripButtonFilter.Enabled = true;
            }
            else
            {
                toolStripButtonFilter.Enabled = false;
            }

            if (comboBoxFilters.SelectedIndex == 0)
            {
                m_filterCurIndex=comboBoxFilters.SelectedIndex;
                m_filter = null;
            }
            else if (comboBoxFilters.SelectedIndex == (comboBoxFilters.Items.Count - 1))
            {
                comboBoxFilters.SelectedIndex = m_filterCurIndex;

                FormFilter formConfig = new FormFilter();
                DialogResult dr = formConfig.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    SnifferFilter filter = formConfig.GetFilter();
                    if (filter!=null)
                    {
                        comboBoxFilters.Items.Insert((comboBoxFilters.Items.Count - 2), filter);
                    }
                }
            }
            else
            {

                m_filterCurIndex=comboBoxFilters.SelectedIndex;
                m_filter = (SnifferFilter)comboBoxFilters.SelectedItem;
            }

        }

        private void toolStripComboBoxParser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxParser.SelectedIndex <= 0 ||
                 toolStripComboBoxParser.SelectedIndex == (toolStripComboBoxParser.Items.Count - 1))
            {
                toolStripButtonCompile.Enabled = false;
                toolStripButtonEditParser.Enabled = false;
            }
            else
            {
                toolStripButtonCompile.Enabled = true;
                toolStripButtonEditParser.Enabled = true;
            }

            if(toolStripComboBoxParser.SelectedIndex == (toolStripComboBoxParser.Items.Count - 1))
            {
                toolStripComboBoxParser.SelectedIndex = m_ParserCurIndex;

                //add new 
                //
                FormParserEditor editor = new FormParserEditor();
                editor.ShowDialog();
                if (editor.ParserPathFileName.Length != 0)
                {
                    ParserBase         o = editor.ParserCompiled;
                    SharpSnifferParser p = new SharpSnifferParser(editor.ParserPathFileName, 
                        Path.GetFileName(editor.ParserPathFileName), o);
                    toolStripComboBoxParser.Items.Insert((toolStripComboBoxParser.Items.Count - 2), p);
                }

            }
            else
            {
                m_ParserCurIndex = toolStripComboBoxParser.SelectedIndex ;
                m_parser = ((SharpSnifferParser)toolStripComboBoxParser.SelectedItem).Parser;
            }
        }

        private void toolStripButtonCompile_Click(object sender, EventArgs e)
        {
            SharpSnifferParser po = (SharpSnifferParser)toolStripComboBoxParser.SelectedItem;
            ParserBase pp =  CompileParserFromFile(po.PathFileName);
            if (pp != null)
            {
                po.Parser = pp;
            }

        }

        private void toolStripButtonEditParser_Click(object sender, EventArgs e)
        {
            FormParserEditor editor = new FormParserEditor();
            editor.ParserPathFileName = ((SharpSnifferParser)toolStripComboBoxParser.SelectedItem).PathFileName;
            editor.ShowDialog();
        }

    }
}
