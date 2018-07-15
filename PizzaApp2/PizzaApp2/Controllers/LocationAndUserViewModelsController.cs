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
    public class LocationAndUserViewModelsController : Controller
    {
        private readonly LocationAndUserContext _context;

        public LocationAndUserViewModelsController(LocationAndUserContext context)
        {
            _context = context;
        }

        // GET: LocationAndUserViewModels
        public async Task<IActionResult> Index()
        {

//            List<LocationAndUserViewModel> lUModel -_context.LocationAndUserViewModel.ToList();

           

    //        var locAndUser = _context.LocationAndUserViewModel.Include(x => x.Locations).Include(x => x.UsersInfoes).AsNoTracking().ToList();
            return View();

           // await _context.LocationAndUserViewModel.ToListAsync()

        }

        // GET: LocationAndUserViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationAndUserViewModel = await _context.LocationAndUserViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationAndUserViewModel == null)
            {
                return NotFound();
            }

            return View(locationAndUserViewModel);
        }

        // GET: LocationAndUserViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationAndUserViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] LocationAndUserViewModel locationAndUserViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationAndUserViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationAndUserViewModel);
        }

        // GET: LocationAndUserViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationAndUserViewModel = await _context.LocationAndUserViewModel.FindAsync(id);
            if (locationAndUserViewModel == null)
            {
                return NotFound();
            }
            return View(locationAndUserViewModel);
        }

        // POST: LocationAndUserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] LocationAndUserViewModel locationAndUserViewModel)
        {
            if (id != locationAndUserViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationAndUserViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationAndUserViewModelExists(locationAndUserViewModel.Id))
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
            return View(locationAndUserViewModel);
        }

        // GET: LocationAndUserViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationAndUserViewModel = await _context.LocationAndUserViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationAndUserViewModel == null)
            {
                return NotFound();
            }

            return View(locationAndUserViewModel);
        }

        // POST: LocationAndUserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationAndUserViewModel = await _context.LocationAndUserViewModel.FindAsync(id);
            _context.LocationAndUserViewModel.Remove(locationAndUserViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationAndUserViewModelExists(int id)
        {
            return _context.LocationAndUserViewModel.Any(e => e.Id == id);
        }
    }
}
