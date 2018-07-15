using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class OrderOfPizza
    {

        public int Id { get; set; }
        public int AmountPizza { get; set; }
        public int IdUsers { get; set; }
        public int IdPizza { get; set; }
        public DateTime DateOrder { get; set; }


       // public IEnumerable<OrderOfPizza> Orders
        //{
           // get { return _context.OrderBy(t => t.Name); }
        //}


    }
}
