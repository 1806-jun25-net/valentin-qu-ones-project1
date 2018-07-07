using System;
using System.Collections.Generic;

namespace Store.Data
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderHasPizza = new HashSet<OrderHasPizza>();
        }

        public int IdPizza { get; set; }
        public string PizzaName { get; set; }
        public int? PiizaCount { get; set; }
        public decimal? PizzaPrice { get; set; }

        public ICollection<OrderHasPizza> OrderHasPizza { get; set; }
    }
}
