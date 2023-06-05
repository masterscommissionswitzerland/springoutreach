using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpringOutreach.Data;
using SpringOutreach.Models;
using SpringOutreach.ViewModels;

namespace SpringOutreach.Controllers
{
    //[Authorize]
    public class PlaceLinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceLinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaceLinks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlaceLink.Include(p => p.Place);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlaceLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlaceLink == null)
            {
                return NotFound();
            }

            var placeLink = await _context.PlaceLink
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeLink == null)
            {
                return NotFound();
            }

            return View(placeLink);
        }

        // GET: PlaceLinks/Create
        public IActionResult Create(int? id)
        {
            var vm = new PlaceLinkViewModel()
            {
                PlaceId = id,
                Place = _context.Place.FirstOrDefault(t => t.Id == id)
            };

            return View(vm);
        }

        // POST: PlaceLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, PlaceLinkViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var placeLink = new PlaceLink()
                {
                    Link = vm.Link,
                    PlaceId = id
                };

                _context.Add(placeLink);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Places", new { id = id });
            }

            return RedirectToAction("Details", "Places", new { id = id });
        }

        // GET: PlaceLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlaceLink == null)
            {
                return NotFound();
            }

            var placeLink = await _context.PlaceLink
                .FirstOrDefaultAsync(t => t.Id == id);

            var vm = new PlaceLinkViewModel()
            {
                PlaceId = id,
                Place = _context.Place.FirstOrDefault(t => t.Id == id),
                Link = placeLink.Link
            };

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: PlaceLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PlaceLinkViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var placeLink = await _context.PlaceLink
                        .Include(t => t.Place)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    placeLink.Link = vm.Link;

                    _context.Update(placeLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Details", "Places", new { id = id });
                }

                return RedirectToAction("Details", "Places", new { id = id });
            }

            return View(vm);
        }

        // GET: PlaceLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlaceLink == null)
            {
                return NotFound();
            }

            var placeLink = await _context.PlaceLink
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeLink == null)
            {
                return NotFound();
            }

            return View(placeLink);
        }

        // POST: PlaceLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.PlaceLink == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlaceLink'  is null.");
            }
            var placeLink = await _context.PlaceLink.FindAsync(id);
            if (placeLink != null)
            {
                _context.PlaceLink.Remove(placeLink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceLinkExists(int? id)
        {
          return (_context.PlaceLink?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Delete Link
        public async Task<RedirectToActionResult> CleanDelete(int? id, int? placeId)
        {
            var placeLink = await _context.PlaceLink.FindAsync(id);
            if (placeLink != null)
            {
                _context.PlaceLink.Remove(placeLink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Places", new { id = placeId });
        }
    }
}
