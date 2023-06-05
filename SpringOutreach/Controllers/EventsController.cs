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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Random random = new Random();

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Event
                .Include(t => t.Outreach);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var @event = await _context.Event
                .Include(t => t.Outreach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create(int? id, int? placeId)
        {
            var vm = new EventViewModel()
            {
                OutreachId = id,
                Outreach = _context.Outreach.FirstOrDefault(t => t.Id == id),
                PlaceId = placeId
            };

            return View(vm);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, int? placeId, EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var stringId = RandomString(12);
                var newevent = new Event()
                {
                    OutreachId = id,
                    Title = vm.Title,
                    Note = vm.Note,
                    Date = vm.Date,
                    Contact = vm.Contact,
                    StringId = stringId,
                    Time = vm.Time,
                    IsInputRequired = vm.IsInputRequired
                };

                _context.Add(newevent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Places", new { id = placeId });
            }

            return View(vm);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var newevent = await _context.Event
                .Include(t => t.Outreach)
                .FirstOrDefaultAsync(m => m.Id == id);

            var vm = new EventViewModel()
            {
                Id = newevent.Id,
                PlaceId = newevent.Outreach.PlaceId,
                Title = newevent.Title,
                Note = newevent.Note,
                Date = newevent.Date,
                Contact = newevent.Contact,
                Time = newevent.Time,
                IsInputRequired = newevent.IsInputRequired
            };

            if (newevent == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newEvent = await _context.Event
                        .Include(t => t.Outreach)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    newEvent.Title = vm.Title;
                    newEvent.Date = vm.Date;
                    newEvent.Note = vm.Note;
                    newEvent.Contact = vm.Contact;
                    newEvent.Time = vm.Time;
                    newEvent.IsInputRequired = vm.IsInputRequired;

                    _context.Update(newEvent);

                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }
                
                return RedirectToAction("Details", "Places", new { id = vm.PlaceId });
            }

            return View(vm);
        }

    // GET: Events/Delete/5
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(t => t.Outreach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Event == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Event'  is null.");
            }
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int? id)
        {
          return (_context.Event?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Random String Method
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
