using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace SharpSniffer
{
    public class SharpSnifferParser
    {
        public string PathFileName
        {
            get
            {
                return m_PathFileName;
            }    
        }

        public ParserBase Parser
        {
            get
            {
                return m_parser;
            }
            set
            {
                m_parser = value;
            }
        }

        private string  m_PathFileName;
        private string  m_Name;
        private ParserBase m_parser;
        
        public SharpSnifferParser(String PathfileName, String name, ParserBase parser )
        {
            m_PathFileName = PathfileName;
            m_Name = name;
            m_parser = parser;
        }
      
        public override string ToString()
        {
            return m_Name;
        }
     
    }

   
}
