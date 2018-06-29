using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Pizza.Library.Pizza
{
    public class Pizza
    {
        [XmlAttribute]
        public int Id { get; set; }
        public Ingredients toppings { get; set; }
        public double cost { get; set; }



    }
}
