using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Pizza.Library.Pizza;

namespace Pizza.Library
{
    [Serializable()]
    public class Orders: IComparable<Orders>
    {
        [XmlAttribute]
        
        public int Id { get; set; }
        public string Location { get; set; }
        public User User { get; set; }
        public Pizza.Pizza Pizza { get; set; }
        public int AmountOfPizza { get; set; }
        public DateTime Date { get; set; }

        public int CompareTo(Orders obj)
        {
            int compare;
            compare = DateTime.Compare(this.Date, obj.Date);

            //  
            compare = -compare;
            return compare;

        }




        
    }
}
