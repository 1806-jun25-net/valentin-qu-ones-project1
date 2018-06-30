using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Pizza.Library.Pizza;

namespace Pizza.Library
{
    public class Orders
    {
        [XmlAttribute]
        public User User { get; set; }
        public Pizza.Pizza Pizza { get; set; }
        public int amountOfPizza { get; set; }

        public static implicit operator Orders(int v)
        {
            throw new NotImplementedException();
        }
    }
}
