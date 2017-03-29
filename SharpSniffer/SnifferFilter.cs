using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Net.Sockets;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace SharpSniffer
{
    public class FilterAddress
    {
        public IPAddress ip;
        public IPAddress mask;

        public FilterAddress(string _ip)
        {
            ip = IPAddress.Parse(_ip);
            mask = new IPAddress(0xffffffff);
        }

        public FilterAddress(string _ip, string _mask)
        {
            ip      = IPAddress.Parse(_ip);
            mask    = IPAddress.Parse(_mask);
        }
        public FilterAddress(IPAddress _ip, IPAddress _mask)
        {
            ip          = _ip;
            mask        = _mask;
        }
        public bool IsMe(IPAddress _ip)
        {
            if ((ip.Address & mask.Address) == (_ip.Address & mask.Address))
            {
                return true;
            }

            return false;
        }
        public override string ToString()
        {
            return ip.ToString() + "/" + mask.ToString();
        }
    };


    public class SnifferFilter
    {
        public List<ProtocolType>   ProtocolList;
        public List<FilterAddress>  AddressList;
        public bool                 AddressExcluded;
        public List<ushort>         Portlist;
        public bool                 PortExluded;
        public bool                 DataOnly;
        public String               FilePathName;
        public String               Name;

        public override string ToString()
        {
            return Name;
        }

        public SnifferFilter()
        {
            DataOnly        = false;
            PortExluded     = false;
            AddressExcluded = false;
            ProtocolList    = new List<ProtocolType>();
            AddressList     = new List<FilterAddress>();
            Portlist        = new List<ushort>();
            FilePathName    = "";
            Name            = "Unname";
        }

        public bool IncludedPort(ushort _port)
        {
            return Portlist.Contains(_port);
        }

        public bool IncludedAddress(string _ip)
        {
            return IncludedAddress(IPAddress.Parse(_ip));
        }

        public bool IncludedAddress(IPAddress _ip)
        {
            bool bfound = false ;
            foreach (FilterAddress addr in AddressList)
            {
                if (addr.IsMe(_ip))
                {
                    bfound = true;
                    break;
                }
            }
            return bfound;
        }

        public bool IncludedProtocol(ProtocolType _proto)
        {
            return ProtocolList.Contains(_proto);
        }

        public bool Save(String szFileName)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(xmlDeclaration);
            XmlElement root =  xdoc.CreateElement("filter");
            xdoc.AppendChild(root);
            XmlAttribute att = xdoc.CreateAttribute("PortExluded");
            att.Value = PortExluded.ToString();
            root.Attributes.Append(att);

            att = xdoc.CreateAttribute("AddressExcluded");
            att.Value = AddressExcluded.ToString();
            root.Attributes.Append(att);

            att = xdoc.CreateAttribute("DataOnly");
            att.Value = DataOnly.ToString();
            root.Attributes.Append(att);

            XmlElement protos= xdoc.CreateElement("protocollist");
            root.AppendChild(protos);

            foreach (ProtocolType proto in ProtocolList)
            {
                XmlElement elem = xdoc.CreateElement("ProtocolType");
                elem.InnerText = proto.ToString();
                protos.AppendChild(elem);
            }


            XmlElement ports = xdoc.CreateElement("portlist");
            root.AppendChild(ports);

            foreach (ushort port in Portlist)
            {
                XmlElement elem = xdoc.CreateElement("port");
                elem.InnerText = port.ToString();
                ports.AppendChild(elem);
            }

            XmlElement ips = xdoc.CreateElement("iplist");
            root.AppendChild(ips);

            foreach (FilterAddress addr in AddressList)
            {
                XmlElement elem = xdoc.CreateElement("ip");
                elem.InnerText = addr.ToString();
                ips.AppendChild(elem);
            }

            xdoc.Save(szFileName);
            FilePathName = szFileName;
            Name = System.IO.Path.GetFileName(szFileName);
            Name = Name.ToLower().Replace(".flt", "");
            return true;
            
        }


        public bool Load(String szFileName)
        {
            
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(szFileName);
            XmlElement root = xdoc.DocumentElement;
            XmlAttribute att =  root.Attributes["AddressExcluded"];
            PortExluded = Convert.ToBoolean(att.Value);
            
            att = root.Attributes["AddressExcluded"];
            AddressExcluded = Convert.ToBoolean(att.Value);

            att = root.Attributes["DataOnly"];
            DataOnly = Convert.ToBoolean(att.Value);


            foreach (XmlElement child in root.ChildNodes)
            {
                //XmlElement protolist = root.ChildNodes["protocollist"];
                if (child.Name == "protocollist")
                {
                    ProtocolList.Clear();
                    foreach (XmlElement elem in child.ChildNodes)
                    {
                        ProtocolType type;
                        if (elem.InnerText == "Tcp")
                            type = ProtocolType.Tcp;
                        else if (elem.InnerText == "Udp")
                            type = ProtocolType.Udp;
                        else if (elem.InnerText == "Icmp")
                            type = ProtocolType.Icmp;
                        else
                            type = ProtocolType.Unknown;
                        ProtocolList.Add(type);
                    }
                }
                else if (child.Name == "portlist")
                {
                    foreach (XmlElement elem in child.ChildNodes)
                    {
                        Portlist.Add(Convert.ToUInt16(elem.InnerText));
                    }
                }
                else if (child.Name == "iplist")
                {
                    foreach (XmlElement elem in child.ChildNodes)
                    {
                        String[] ss = elem.InnerText.Split('/');
                        AddressList.Add(new FilterAddress(IPAddress.Parse(ss[0]),
                            IPAddress.Parse(ss[1])));
                    }
                }
            }

            FilePathName = szFileName;
            Name = System.IO.Path.GetFileName(szFileName);
            Name = Name.ToLower().Replace(".flt", "");
            return true;

        }
    };
}
