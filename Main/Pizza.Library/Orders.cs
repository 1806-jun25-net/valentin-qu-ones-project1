using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Pizza.Library.Pizza;

namespace Pizza.Library
{
    public class Orders: IComparable<Orders>
    {
        [XmlAttribute]
        public int Id { get; set; }
        public string location { get; set; }
        public User User { get; set; }
        public Pizza.Pizza Pizza { get; set; }
        public int amountOfPizza { get; set; }
        public DateTime date { get; set; }

        public int CompareTo(Orders obj)
        {
            int compare;
            compare = DateTime.Compare(this.date, obj.date);

            //  
            compare = -compare;
            return compare;

        }




        
    }
}
