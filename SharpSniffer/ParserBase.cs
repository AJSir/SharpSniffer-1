using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;

namespace SharpSniffer
{
    public class ParserBase
    {
        private static string map = "0123456789ABCDEF";
        public static string BinToHexString(byte[] arr)
        {
            if (arr == null) return "";
            StringBuilder str = new StringBuilder();
            foreach (byte b in arr)
            {
                char lsb = map[(b & 0x0f)];
                char msb = map[((b >> 4) & 0x0f)];
                str.Append(msb);
                str.Append(lsb);
                str.Append(' ');
            }

            return str.ToString();
        }

        public virtual void OnPacket(RichTextBox rtfedit, IPHeader ipHdr, TCPHeader tcpHdr, UDPHeader udpHdr)
        {
            ushort datalen = 0;
            Font savefont =  new Font(rtfedit.SelectionFont,FontStyle.Regular);
            Font ipfont = new Font(savefont, FontStyle.Underline | FontStyle.Bold);
            //
            rtfedit.SelectionFont = ipfont;

            rtfedit.SelectionColor = Color.Cyan;
            rtfedit.AppendText(String.Format("{0,5} ", ipHdr.Protocol.ToString()));
         
            
         
            
            if (udpHdr != null || tcpHdr != null)
            {
                byte[] data = (tcpHdr == null) ? udpHdr.Data : tcpHdr.Data;
                datalen = (ushort)((data == null) ? 0 : data.Length);
                ushort srcport = (tcpHdr == null) ? udpHdr.SourcePort : tcpHdr.SourcePort;
                ushort destport = (tcpHdr == null) ? udpHdr.DestinationPort : tcpHdr.DestinationPort;
                
                //
                rtfedit.SelectionColor = Color.Green;
                rtfedit.AppendText(String.Format("[{0,-15}:{1,-5}]", ipHdr.SourceAddress, srcport));

                //
                rtfedit.SelectionFont = ipfont;
                rtfedit.SelectionColor = Color.Blue;
                rtfedit.AppendText(String.Format(" [{0,-15}:{1,-5}]", ipHdr.DestinationAddress, destport));

                if(tcpHdr != null)
                {
                    //
                    rtfedit.SelectionColor = Color.DarkOrange;
                    rtfedit.AppendText(String.Format(" [{0,-3}]", tcpHdr.FlagsString));
                }

                if (datalen != 0)
                {
                    rtfedit.SelectionColor = Color.Red;
                    rtfedit.AppendText(String.Format(" [{0,-5}]", datalen));
                }

               

                //data
                rtfedit.SelectionFont = savefont;
                rtfedit.SelectionColor = Color.Black;
                rtfedit.AppendText(" " + BinToHexString(data) + "\r\n");

                return;
            }

            datalen = (ushort)ipHdr.Data.Length;
            
            rtfedit.SelectionColor = Color.Green;
            rtfedit.AppendText(String.Format("S[{0,-15}]", ipHdr.SourceAddress));

            //
            rtfedit.SelectionFont = savefont;
            rtfedit.AppendText(" ");


            rtfedit.SelectionColor = Color.Blue;
            rtfedit.AppendText(String.Format("D[{0,-15}]", ipHdr.DestinationAddress));

            if (datalen != 0)
            {
                rtfedit.SelectionColor = Color.Red;
                rtfedit.AppendText(String.Format(" DATA[{0,-5}]", datalen));
            }

            //data
            rtfedit.SelectionFont = savefont;
            rtfedit.SelectionColor = Color.Black;
            rtfedit.AppendText(" "+BinToHexString(ipHdr.Data)+"\r\n");

        }
    }
}
