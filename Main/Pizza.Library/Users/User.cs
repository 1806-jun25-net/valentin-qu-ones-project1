using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Pizza.Library
{
    public class User
    {

        public int Id { get; set; }
        public Name Name { get; set; }
        public string Email {get; set;}
        public Address Address { get; set; }
        


    }
}
