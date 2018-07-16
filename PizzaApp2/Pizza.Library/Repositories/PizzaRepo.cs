using PizzaApp2.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza.Library.Repositories
{
    class PizzaRepo
    {


        private readonly ApplicationDbContext _db;

        public PizzaRepo(PizzaRepo db)
        {
            
        }



        public IEnumerable<PizzaApp2.Models.Pizza> GetPizzas()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var pizza = _db.Pizza.AsNoTracking().ToList();
            return pizza;

        }


        public IEnumerable<PizzaApp2.Models.Pizza> GetByID(int id)
        {
            var pizz = _db.Pizza.Find(id);

            yield return pizz;
        }

        public IEnumerable<PizzaApp2.Models.UserInfo> GetUserInfo()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var userinfo = _db.UserInfo.AsNoTracking().ToList();
            return userinfo;

        }

        public IEnumerable<PizzaApp2.Models.Locations> GetLocations()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var locations = _db.Location.AsNoTracking().ToList();
            return locations;

        }


        



        public IEnumerable<PizzaApp2.Models.Orders> GetOrders()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var orderHas = _db.Orders.AsNoTracking().ToList();
            return orderHas;

        }




    }

}
