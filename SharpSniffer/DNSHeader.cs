using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;


    public class DNSHeader
    {
        //DNS header fields
        public ushort usIdentification;        //Sixteen bits for identification
        public ushort usFlags;                 //Sixteen bits for DNS flags
        public ushort usTotalQuestions;        //Sixteen bits indicating the number of entries 
                                                //in the questions list
        public ushort usTotalAnswerRRs;        //Sixteen bits indicating the number of entries
                                                //entries in the answer resource record list
        public ushort usTotalAuthorityRRs;     //Sixteen bits indicating the number of entries
                                                //entries in the authority resource record list
        public ushort usTotalAdditionalRRs;    //Sixteen bits indicating the number of entries
                                                //entries in the additional resource record list
        //End DNS header fields

        public DNSHeader(byte []byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);    
   
            //First sixteen bits are for identification
            usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Next sixteen contain the flags
            usFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Read the total numbers of questions in the quesion list
            usTotalQuestions = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Read the total number of answers in the answer list
            usTotalAnswerRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Read the total number of entries in the authority list
            usTotalAuthorityRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            //Total number of entries in the additional resource record list
            usTotalAdditionalRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
        }

    
	}

