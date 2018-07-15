using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PizzaApp2.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext (DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaApp2.Models.Orders> Orders { get; set; }
        public DbSet<PizzaApp2.Models.UserInfo> UserInfo { get; set; }
        public DbSet<PizzaApp2.Models.Locations> Locations { get; set; }

        public DbSet<PizzaApp2.Models.Pizza> Pizza { get; set; }


    }
}
