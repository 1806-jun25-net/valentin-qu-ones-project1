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

        public void AddOrderHasPizza()
        {
            /*
            var idForOrderHasPizza = GetOrders();

            var id1 = _db.OrderHasPizza.FirstOrDefault(g => g.OrderIdOrder == iD);
            if (id == null)
            {
                throw new ArgumentException("order not added", nameof(id));

            }
            var orderHasPizza = new OrderHasPizza
            {
                OrderIdOrder = idForOrderHasPizza.Last().IdOrder + 1,
                OrderLocationIdLocation = location,
                OrderUserIdUser = userID,
                OrderUserLocationIdLocation = userLocation,
                PizzaIdPizza = idPizza,
                AmountOfPizzaInOrder = amountPizza





            };




            _db.Add(orderHasPizza);
            */

        }

        public void AddOrder(int location,int userID, int userLocation, DateTime release, int idPizza, int amountPizza)
        {

            //LINQ first fails by thrrowing exception
            //FirstOrDefault fails to just null
            
            var order = new Orders
            {

                LocationIdLocation = location,
                UserIdUser = userID,
                UserLocationIdLocation = userLocation,
                DateOfOrders = release,
                
                
                

            };
            _db.Add(order);

            
        }

        public int getLastIndexOrder()
        {
            var idForOrderHasPizza = GetOrders();
            var lastIndex = idForOrderHasPizza.Last().IdOrder;
            return lastIndex;

        }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
