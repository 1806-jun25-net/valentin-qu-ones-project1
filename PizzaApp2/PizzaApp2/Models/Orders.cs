using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class Orders
    {

        public int Id { get; set; }
        [Range(1,12)]
        public int AmountPizza { get; set; }
        public int User_idUser { get; set; }
        public int Pizza_IdPizza { get; set; }
        public int User_Location_IdLocation { get; set; }
        public DateTime DateOrder { get; set; }


    }
}
