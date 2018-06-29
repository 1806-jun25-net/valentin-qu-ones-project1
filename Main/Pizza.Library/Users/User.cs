using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Pizza.Library
{
    public class User
    {
        [XmlAttribute]
        public int Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public int Age { get; set; }


    }
}
