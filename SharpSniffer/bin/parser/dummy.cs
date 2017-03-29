
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SharpSniffer;
using System.Drawing;
using System.Windows.Forms;

public class TCPCONNCLOSEPARSER:ParserBase
{
    private int recvnum = 0;
    public override void OnPacket(RichTextBox rtfedit, IPHeader ipHdr, TCPHeader tcpHdr, UDPHeader udpHdr)
    {
	        ushort datalen = 0;
            Font savefont =  new Font(rtfedit.SelectionFont,FontStyle.Regular);
            Font ipfont = new Font(savefont, FontStyle.Underline | FontStyle.Bold);
            //
            rtfedit.SelectionFont = ipfont;

            //rtfedit.SelectionColor = Color.Cyan;
            //rtfedit.AppendText(String.Format("{0,5} ", ipHdr.Protocol.ToString()));
         
            
            if (tcpHdr != null)
            {
                byte[] data = (tcpHdr == null) ? udpHdr.Data : tcpHdr.Data;
                datalen = (ushort)((data == null) ? 0 : data.Length);
                if (datalen != 0) return;

                rtfedit.SelectionColor = Color.Cyan;
                rtfedit.AppendText(String.Format("{0,-8} ", recvnum++));

             

                ushort srcport = (tcpHdr == null) ? udpHdr.SourcePort : tcpHdr.SourcePort;
                ushort destport = (tcpHdr == null) ? udpHdr.DestinationPort : tcpHdr.DestinationPort;
                
                //
                rtfedit.SelectionColor = Color.Green;
                rtfedit.AppendText(String.Format("[{0,-15}:{1,-5}]", ipHdr.SourceAddress, srcport));

                //
                rtfedit.SelectionFont = ipfont;
                rtfedit.SelectionColor = Color.Blue;
                rtfedit.AppendText(String.Format(" [{0,-15}:{1,-5}]", ipHdr.DestinationAddress, destport));
                
                //
                rtfedit.SelectionColor = Color.DarkOrange;
                rtfedit.AppendText(String.Format(" [{0,-3}]\r\n", tcpHdr.FlagsString));


                return;
            }


    }
}
