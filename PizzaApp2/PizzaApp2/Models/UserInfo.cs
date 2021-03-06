﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp2.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public int Location_IdLocation { get; set; }
        [Required]
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string passwords { get; set; }

    }
}
