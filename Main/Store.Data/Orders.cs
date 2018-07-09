using System;
using System.Collections.Generic;

namespace Store.Data
{
    public partial class Orders
    {
        public Orders()
        {
            OrderHasPizza = new HashSet<OrderHasPizza>();
        }

        public int IdOrder { get; set; }
        public int LocationIdLocation { get; set; }
        public int UserIdUser { get; set; }
        public int UserLocationIdLocation { get; set; }
        public DateTime? DateOfOrders { get; set; }

        public Location LocationIdLocationNavigation { get; set; }
        public ICollection<OrderHasPizza> OrderHasPizza { get; set; }
    }
}
