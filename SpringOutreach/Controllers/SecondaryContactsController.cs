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
    public class SecondaryContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Random random = new Random();

        public SecondaryContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SecondaryContacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SecondaryContact.Include(s => s.Place);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SecondaryContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SecondaryContact == null)
            {
                return NotFound();
            }

            var secondaryContact = await _context.SecondaryContact
                .Include(s => s.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryContact == null)
            {
                return NotFound();
            }

            return View(secondaryContact);
        }

        // GET: SecondaryContacts/Create
        public IActionResult Create(int? id)
        {
            var vm = new SecondaryContactViewModel()
            {
                PlaceId = id,
                Place = _context.Place.FirstOrDefault(t => t.Id == id),
                SecondaryContactPosition = _context.Position
                .ToList()
            };

            return View(vm);
        }

        // POST: SecondaryContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, SecondaryContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var stringId = RandomString(12);
                var secondarycontact = new SecondaryContact()
                {
                    Name = vm.Name,
                    Mail = vm.Mail,
                    Phone = vm.Phone,
                    PlaceId = id,
                    ResponsibleFor = vm.ResponsibleFor,
                    StringId = stringId,
                    SecondaryContactPositionId = vm.SecondaryContactPositionId
                };

                _context.Add(secondarycontact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Places", new { id = id });
            }

            return RedirectToAction("Details", "Places", new { id = id });
        }

        // GET: SecondaryContacts/Edit/5
        public async Task<IActionResult> Edit(int? id, int? placeId)
        {
            if (id == null || _context.SecondaryContact == null)
            {
                return NotFound();
            }

            var secondaryContact = await _context.SecondaryContact
                .FirstOrDefaultAsync(t => t.Id == id);

            var vm = new SecondaryContactViewModel()
            {
                PlaceId = placeId,
                Place = _context.Place.FirstOrDefault(t => t.Id == id),
                Name = secondaryContact.Name,
                Mail = secondaryContact.Mail,
                Phone = secondaryContact.Phone,
                SecondaryContactPositionId = secondaryContact.SecondaryContactPositionId,
                ResponsibleFor = secondaryContact.ResponsibleFor,
                SecondaryContactPosition = _context.Position.ToList()
             };

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: SecondaryContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? placeId, SecondaryContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var secondaryContact = await _context.SecondaryContact
                        .Include(t => t.Place)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    secondaryContact.Name = vm.Name;
                    secondaryContact.Mail = vm.Mail;
                    secondaryContact.Phone = vm.Phone;
                    secondaryContact.ResponsibleFor = vm.ResponsibleFor;
                    secondaryContact.SecondaryContactPositionId = vm.SecondaryContactPositionId;

                    _context.Update(secondaryContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Details", "Places", new { id = placeId });
                }

                return RedirectToAction("Details", "Places", new { id = placeId });
            }

            return View(vm);
        }

        // GET: SecondaryContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SecondaryContact == null)
            {
                return NotFound();
            }

            var secondaryContact = await _context.SecondaryContact
                .Include(s => s.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryContact == null)
            {
                return NotFound();
            }

            return View(secondaryContact);
        }

        // POST: SecondaryContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.SecondaryContact == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SecondaryContact'  is null.");
            }
            var secondaryContact = await _context.SecondaryContact.FindAsync(id);
            if (secondaryContact != null)
            {
                _context.SecondaryContact.Remove(secondaryContact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondaryContactExists(int? id)
        {
          return (_context.SecondaryContact?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Random String Method
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Delete Secondary Contact
        public async Task<RedirectToActionResult> CleanDelete(int? id, int? placeId)
        {
            var secondaryContact = await _context.SecondaryContact.FindAsync(id);
            if (secondaryContact != null)
            {
                _context.SecondaryContact.Remove(secondaryContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Places", new { id = placeId });
        }
    }
}
