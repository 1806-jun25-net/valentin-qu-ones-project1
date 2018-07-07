using System;
using System.Collections.Generic;

namespace Store.Data
{
    public partial class UserInfo
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationIdLocation { get; set; }

        public Location LocationIdLocationNavigation { get; set; }
    }
}
