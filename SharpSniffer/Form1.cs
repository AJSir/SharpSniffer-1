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

namespace SharpSniffer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int SIO_RCVALL = unchecked((int)0x98000001);//监听所有的数据包
        private byte[] m_Buffer = new byte[65535];//保存接收數據

        private void button1_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw);

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.2.201"), 0);
            s.Blocking = false;
            s.Bind(ep);

            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            byte[] IN = new byte[4] { 1, 0, 0, 0 };
            byte[] OUT = new byte[4];
            
            //int ret_code = s.IOControl(SIO_RCVALL, IN, OUT);
            s.IOControl(IOControlCode.ReceiveAll, IN, OUT);

            AsyncCallback asyncCallback = new AsyncCallback(OnReceiveCallBack);

            s.BeginReceive(m_Buffer, 0, m_Buffer.Length, SocketFlags.None, asyncCallback, s);
        }


        private void OnReceiveCallBack(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int nread = client.EndReceive(ar);
            if (nread > 0)
            {
                richTextBox1.AppendText("1");
            }
            else
            {
                richTextBox1.AppendText("0");
            }

        }   

    }
}
