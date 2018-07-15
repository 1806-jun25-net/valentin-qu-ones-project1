using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PizzaApp2.Models
{
    public class LocationsContext : DbContext
    {
        public LocationsContext (DbContextOptions<LocationsContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaApp2.Models.Locations> Locations { get; set; }
    }
}
