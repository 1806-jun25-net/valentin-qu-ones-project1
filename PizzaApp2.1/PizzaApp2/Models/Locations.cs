using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class Locations
    {

        public int Id { get; set; }
        [Required]
        public string LocationName { get; set; }



    }
}
