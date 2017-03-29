using System.Net;
using System.Text;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

public class IPHeader
{
    //IP Header fields
    private byte      byVersionAndHeaderLength;   //Eight bits for version and header length
    private byte      byDifferentiatedServices;    //Eight bits for differentiated services (TOS)
    private ushort    usTotalLength;              //Sixteen bits for total length of the datagram (header + message)
    private ushort    usIdentification;           //Sixteen bits for identification
    private ushort    usFlagsAndOffset;           //Eight bits for flags and fragmentation offset
    private byte      byTTL;                      //Eight bits for TTL (Time To Live)
    private byte      byProtocol;                 //Eight bits for the underlying protocol
    private short     sChecksum;                  //Sixteen bits containing the checksum of the header
                                                  //(checksum can be negative so taken as short)
    private uint      uiSourceIPAddress;          //Thirty two bit source IP Address
    private uint      uiDestinationIPAddress;     //Thirty two bit destination IP Address
    //End IP Header fields
    
    private byte      byHeaderLength;             //Header length
    private byte[]    byIPData = null;  //Data carried by the datagram


    public IPHeader(byte[] byBuffer, int nReceived)
    {
        
        try
        {
            //Create MemoryStream out of the received bytes
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            //Next we create a BinaryReader out of the MemoryStream
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            //The first eight bits of the IP header contain the version and
            //header length so we read them
            byVersionAndHeaderLength = binaryReader.ReadByte();

            //The next eight bits contain the Differentiated services
            byDifferentiatedServices = binaryReader.ReadByte();

            //Next eight bits hold the total length of the datagram
            usTotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Next sixteen have the identification bytes
            usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Next sixteen bits contain the flags and fragmentation offset
            usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Next eight bits have the TTL value
            byTTL = binaryReader.ReadByte();

            //Next eight represnts the protocol encapsulated in the datagram
            byProtocol = binaryReader.ReadByte();

            //Next sixteen bits contain the checksum of the header
            sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Next thirty two bits have the source IP address
            uiSourceIPAddress = (uint)(binaryReader.ReadInt32());

            //Next thirty two hold the destination IP address
            uiDestinationIPAddress = (uint)(binaryReader.ReadInt32());

            //Now we calculate the header length

            byHeaderLength = byVersionAndHeaderLength;
            //The last four bits of the version and header length field contain the
            //header length, we perform some simple binary airthmatic operations to
            //extract them
            byHeaderLength <<= 4;
            byHeaderLength >>= 4;
            //Multiply by four to get the exact header length
            byHeaderLength *= 4;

            int datalen = usTotalLength - byHeaderLength;
            if (datalen > 0)
            {
                byIPData = new byte[datalen];
                //Copy the data carried by the data gram into another array so that
                //according to the protocol being carried in the IP datagram
                Array.Copy(byBuffer,
                           byHeaderLength,  //start copying from the end of the header
                           byIPData, 0,
                           usTotalLength - byHeaderLength);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "SharpSniffer", MessageBoxButtons.OK, 
                MessageBoxIcon.Error);
        }
    }

    public byte Version
    {
        get
        {
            return (byte)(byVersionAndHeaderLength >> 4);
        }
    }
 

    public byte HeaderLength
    {
        get
        {
            return byHeaderLength;
        }
    }


    public ushort MessageLength
    {
        get
        {
            //MessageLength = Total length of the datagram - Header length
            return (ushort)(usTotalLength - byHeaderLength);
        }
    }

    public byte  DifferentiatedServices
    {
        get
        {
            return byDifferentiatedServices;
        }
    }
   

    public byte Flags
    {
        get
        {
            byte nFlags = (byte)(usFlagsAndOffset >> 13);
            return nFlags;
        }
    }

    
    
    public int FragmentationOffset
    {
        get
        {
            int nOffset = usFlagsAndOffset << 3;
            nOffset >>= 3;
            return nOffset;
        }
    }

    
    public byte TTL
    {
        get
        {
            return byTTL;
        }
    }

 
    public ProtocolType Protocol
    {
        get
        {   //The protocol field represents the protocol in the data portion
            //of the datagram
            //
            return (ProtocolType)byProtocol;
        }
    }

    public short Checksum
    {
        get
        {
            //Returns the checksum in hexadecimal format
            return sChecksum;
        }
    }

    public IPAddress SourceAddress
    {
        get
        {
            return new IPAddress(uiSourceIPAddress);
        }
    }

    public IPAddress DestinationAddress
    {
        get
        {
            return new IPAddress(uiDestinationIPAddress);
        }
    }

    public ushort TotalLength
    {
        get
        {
            return usTotalLength;
        }
    }

    
    public ushort Identification
    {
        get
        {
            return usIdentification;
        }
    }



    public byte[] Data
    {
        get
        {
            return byIPData;
        }
    }

    
}
