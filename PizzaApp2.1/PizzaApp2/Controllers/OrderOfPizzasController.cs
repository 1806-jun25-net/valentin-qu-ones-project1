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
    public class OrderOfPizzasController : Controller
    {
        private readonly OrderHasPizzaContext _context;

        public OrderOfPizzasController(OrderHasPizzaContext context)
        {
            _context = context;
        }

        // GET: OrderOfPizzas
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderOfPizza.ToListAsync());
        }

        // GET: OrderOfPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderOfPizza = await _context.OrderOfPizza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderOfPizza == null)
            {
                return NotFound();
            }

            return View(orderOfPizza);
        }

        // GET: OrderOfPizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderOfPizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountPizza,IdUsers,IdPizza,DateOrder")] OrderOfPizza orderOfPizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderOfPizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderOfPizza);
        }

        // GET: OrderOfPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderOfPizza = await _context.OrderOfPizza.FindAsync(id);
            if (orderOfPizza == null)
            {
                return NotFound();
            }
            return View(orderOfPizza);
        }

        // POST: OrderOfPizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AmountPizza,IdUsers,IdPizza,DateOrder")] OrderOfPizza orderOfPizza)
        {
            if (id != orderOfPizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderOfPizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderOfPizzaExists(orderOfPizza.Id))
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
            return View(orderOfPizza);
        }

        // GET: OrderOfPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderOfPizza = await _context.OrderOfPizza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderOfPizza == null)
            {
                return NotFound();
            }

            return View(orderOfPizza);
        }

        // POST: OrderOfPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderOfPizza = await _context.OrderOfPizza.FindAsync(id);
            _context.OrderOfPizza.Remove(orderOfPizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderOfPizzaExists(int id)
        {
            return _context.OrderOfPizza.Any(e => e.Id == id);
        }
    }
}
