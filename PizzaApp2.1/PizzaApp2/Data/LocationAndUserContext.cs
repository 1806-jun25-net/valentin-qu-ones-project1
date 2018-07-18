using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PizzaApp2.Models
{
    public class LocationAndUserContext : DbContext
    {
        public LocationAndUserContext (DbContextOptions<LocationAndUserContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaApp2.Models.LocationAndUserViewModel> LocationAndUserViewModel { get; set; }
    }
}
