using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class LocationAndUserViewModel
    {

        public int Id { get; set; }
        public IEnumerable<PizzaApp2.Models.Locations> Locations { get; set; }
        public IEnumerable<PizzaApp2.Models.UserInfo> UsersInfoes { get; set; }

        

    }
}
