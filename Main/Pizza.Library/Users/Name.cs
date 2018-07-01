using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Pizza.Library
{
    public class Name
    {
        [XmlAttribute]
        public string First { get; set; }
        public string Last { get; set; }
    }
}
