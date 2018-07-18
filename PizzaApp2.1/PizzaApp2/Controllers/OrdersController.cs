using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApp2.Models;

namespace PizzaApp2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderContext _context;
        private readonly PizzaContext _pizzaContext;

        public OrdersController(OrderContext context)
        {
            _context = context;
        }

        // GET: Orders
        /* public async Task<IActionResult> Index()
         {


             return View(await _context.Orders.ToListAsync());
         }*/

        public IActionResult Index(string sortOrder)
        {


            ViewBag.Quantity = String.IsNullOrEmpty(sortOrder) ? "Quantity" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var ord = from s in _context.Orders
                      select s;
            switch (sortOrder)
            {

                case "Date":
                    ord = ord.OrderBy(s => s.DateOrder);
                    break;
                case "Quantity":
                    ord = ord.OrderBy(s => s.AmountPizza);
                    break;

            }

            return View(ord);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["Location Name"] = (from location1 in _context.Locations where location1.Id == orders.User_Location_IdLocation select location1.LocationName).ToList().FirstOrDefault();
            ViewData["User Name"] = (from user in _context.UserInfo where user.Id == orders.User_idUser select user.FirstName).ToList().FirstOrDefault();
            ViewData["Pizza Name"] = (from piz in _context.Pizza where piz.Id == orders.Pizza_IdPizza select piz.PizzaName).ToList().FirstOrDefault();

            ViewData["Pizza Price"] = (from piz in _context.Pizza where piz.Id == orders.Pizza_IdPizza select piz.PizzaPrice).ToList().FirstOrDefault();

            var pPrice = decimal.Parse(((from piz in _context.Pizza where piz.Id == orders.Pizza_IdPizza select piz.PizzaPrice).ToList().FirstOrDefault()).ToString());

            var price = int.Parse(orders.AmountPizza.ToString()) * pPrice;

            ViewData["Order Price"] = price.ToString();


            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        
        public IActionResult Create()
        {
            ViewBag.LocationList = _context.Locations.ToList();
            ViewBag.PizzaList = _context.Pizza.ToList();
            ViewBag.UserList = _context.UserInfo.ToList();

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountPizza,User_idUser,Pizza_IdPizza,User_Location_IdLocation,DateOrder")] Orders orders)
        {

            var pizzaAm = int.Parse(orders.AmountPizza.ToString());

            if (pizzaAm > 12)
            {


                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {





                    var pizzaOb = int.Parse((from pizza in _context.Pizza where pizza.Id == orders.Pizza_IdPizza select pizza.PiizaCount).ToList().FirstOrDefault().ToString());
                    var author = _context.Pizza.First(a => a.Id == orders.Pizza_IdPizza);
                    var total = pizzaOb - pizzaAm;
                    author.PiizaCount = total;
                    await _context.SaveChangesAsync();

                    _context.Add(orders);
                    await _context.SaveChangesAsync();







                    return RedirectToAction(nameof(Index));
                }
                return View(orders);
            }
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.LocationList = _context.Locations.ToList();
            ViewBag.PizzaList = _context.Pizza.ToList();
            ViewBag.UserList = _context.UserInfo.ToList();
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AmountPizza,User_idUser,Pizza_IdPizza,User_Location_IdLocation,DateOrder")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
