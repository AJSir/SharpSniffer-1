using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;


    public class UDPHeader
    {
        //UDP header fields
        private ushort usSourcePort;            //Sixteen bits for the source port number        
        private ushort usDestinationPort;       //Sixteen bits for the destination port number
        private ushort usLength;                //Length of the UDP header
        private short sChecksum;                //Sixteen bits for the checksum
                                                //(checksum can be negative so taken as short)              
        private ushort usMessageLength;           //Length of the data being carried
        //End UDP header fields

        private byte[] byUDPData = null;  //Data carried by the UDP packet

        public UDPHeader(byte [] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            //The first sixteen bits contain the source port
            usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //The next sixteen bits contain the destination port
            usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //The next sixteen bits contain the length of the UDP packet
            usLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //The next sixteen bits contain the checksum
            sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Copy the data carried by the UDP packet into the data buffer
            int datalen = nReceived - 8;
            usMessageLength = 0;
            if (datalen > 0)
            {
                usMessageLength = (ushort)datalen;
                byUDPData = new byte[datalen];
                Array.Copy(byBuffer,
                           8,               //The UDP header is of 8 bytes so we start copying after it
                           byUDPData,
                           0,
                           nReceived - 8);
            }
        }

        public ushort SourcePort
        {
            get
            {
                return usSourcePort;
            }
        }

        public ushort DestinationPort
        {
            get
            {
                return usDestinationPort;
            }
        }

        public ushort Length
        {
            get
            {
                return usLength;
            }
        }

        public short Checksum
        {
            get
            {
                //Return the checksum in hexadecimal format
                return sChecksum;
            }
        }

        public byte[] Data
        {
            get
            {
                return byUDPData;
            }
        }

        public ushort MessageLength
        {
            get
            {
                return usMessageLength;
            }
        }
    }
