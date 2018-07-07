using System;
using System.Collections.Generic;

namespace Store.Data
{
    public partial class Location
    {
        public Location()
        {
            Orders = new HashSet<Orders>();
            UserInfo = new HashSet<UserInfo>();
        }

        public int IdLocation { get; set; }
        public string LocationName { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<UserInfo> UserInfo { get; set; }
    }
}
