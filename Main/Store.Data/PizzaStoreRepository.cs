using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Data
{
    public class PizzaStoreRepository
    {

        private readonly PizzaPalaceContext _db;

        public PizzaStoreRepository(PizzaPalaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }



        public IEnumerable<Pizza> GetPizzas()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var pizza = _db.Pizza.AsNoTracking().ToList();
            return pizza;

        }


        public IEnumerable<Pizza> GetByID(int id)
        {
            var pizz = _db.Pizza.Find(id);

            yield return pizz;
        }

        public IEnumerable<UserInfo> GetUserInfo()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var userinfo = _db.UserInfo.AsNoTracking().ToList();
            return userinfo;

        }

        public IEnumerable<Location> GetLocations()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var locations = _db.Location.AsNoTracking().ToList();
            return locations;

        }


        public IEnumerable<OrderHasPizza> GetOrderHasPizzas()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var orderHas = _db.OrderHasPizza.AsNoTracking().ToList();
            return orderHas;

        }



        public IEnumerable<Orders> GetOrders()
        {
            //we dont need to track changes to these
            //so skip the overhead of doing so
            var orderHas = _db.Orders.AsNoTracking().ToList();
            return orderHas;

        }

    }
}
