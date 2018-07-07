using System;
using System.Collections.Generic;

namespace Store.Data
{
    public partial class OrderHasPizza
    {
        public int OrderIdOrder { get; set; }
        public int OrderLocationIdLocation { get; set; }
        public int OrderUserIdUser { get; set; }
        public int OrderUserLocationIdLocation { get; set; }
        public int PizzaIdPizza { get; set; }
        public int IdOrderHasPizza { get; set; }
        public int AmountOfPizzaInOrder { get; set; }

        public Orders Order { get; set; }
        public Pizza PizzaIdPizzaNavigation { get; set; }
    }
}
