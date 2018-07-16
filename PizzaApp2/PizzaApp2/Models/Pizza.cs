using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class Pizza
    {

        public int Id { get; set; }
        [Required]
        public string PizzaName { get; set; }
        [Required]
        public int PiizaCount { get; set; }
        [Required]
        public decimal PizzaPrice { get; set; }



    }
}
