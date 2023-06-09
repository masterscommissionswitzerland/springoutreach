using System.Linq;
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
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Places
        public async Task<IActionResult> Index()
        {
            var places = await _context.Place
                .Include(t => t.PlaceType)
                .Include(t => t.Contact)
                .Include(t => t.Outreaches)
                .ThenInclude(t => t.Status)
                .Select(x => new Place
                {
                    Name = x.Name,
                    City = x.City,
                    Canton = x.Canton,
                    Contact = x.Contact,
                    Id = x.Id,
                    Outreaches = new List<Outreach> { x.Outreaches.OrderByDescending(x => x.Year).FirstOrDefault() }
                })
                .ToListAsync();

            //.OrderByDescending(OutreachYear => OutreachYear.Year)
            //.Take(1))

            return View(places);
        }

        // GET: Places/MailingList
        public async Task<IActionResult> MailingList()
        {
            var place = await _context.Place
                .Include(t => t.PlaceType)
                .Include(t => t.Contact)
                .Include(t => t.SecondaryContacts)
                .ToListAsync();

            return View(place);
        }

        // GET: SecondaryContacts/AllContacts/
        public async Task<IActionResult> AllContacts()
        {
            var place = await _context.Place
                .Include(t => t.Contact)
                .Include(t => t.SecondaryContacts)
                .ToListAsync();

            return View(place);
        }

        // GET: Places/CurrentOutreaches
        public async Task<IActionResult> CurrentOutreaches()
        {
            var places = await _context.Place
                .Include(t => t.Outreaches)
                .ThenInclude(t => t.Status)
                .Select(x => new Place
                {
                    Name = x.Name,
                    Id = x.Id,
                    Outreaches = new List<Outreach> { x.Outreaches.OrderByDescending(x => x.Year).FirstOrDefault() }
                })
                .ToListAsync();

            return View(places);
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .Include(t => t.PlaceType)
                .Include(t => t.Contact)
                .ThenInclude(m => m.Position)
                .Include(t => t.SecondaryContacts)
                .ThenInclude(m => m.SecondaryContactPosition)
                .Include(t => t.Outreaches)
                .ThenInclude(t => t.PdfFile)
                .Include(t => t.Outreaches)
                .ThenInclude(t => t.Events)
                .Include(t => t.Outreaches)
                .ThenInclude(t => t.Status)
                .Include(t => t.PlaceLinks)
                .FirstOrDefaultAsync(t => t.Id == id);

            var vm = new PlaceViewModel()
            {
                Id = place.Id,
                Name = place.Name,
                City = place.City,
                Canton = place.Canton,
                Adress = place.Adress,
                SetPlaceType = place.PlaceType.Title,
                Contact = place.Contact,
                Outreaches = place.Outreaches,
                SecondaryContacts = place.SecondaryContacts,
                PlaceNote = place.PlaceNote,
                PlaceConnection = place.PlaceConnection,
                PlaceLinks = place.PlaceLinks
            };

            vm.CurrentOutreach = place.Outreaches
                 .OrderByDescending(OutreachYear => OutreachYear.Year)
                 .Take(1)
                 .FirstOrDefault();

            if (place == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            var vm = new PlaceViewModel()
            {
                PlaceTypes = _context.Type
                .OrderBy(x => x.Id)
                .ToList(),
                Position = _context.Position
                .OrderBy(x => x.Id)
                .ToList(),
            };
            return View(vm);
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var place = new Place()
                {
                    Name = vm.Name,
                    City = vm.City, 
                    Canton = vm.Canton,
                    Adress = vm.Adress,
                    PlaceConnection = vm.PlaceConnection,
                    PlaceNote = vm.PlaceNote,
                    PlaceTypeId = vm.PlaceTypeId
                };

                _context.Add(place);
                await _context.SaveChangesAsync();

                var contact = new Contact()
                {
                    Name = vm.ContactName,
                    Mail = vm.ContactMail,
                    Phone = vm.ContactPhone,
                    ResponsibleFor = vm.ResponsibleFor,
                    PositionId = vm.PositionId,
                    PlaceId = place.Id
                };

                _context.Add(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .Include(t => t.Contact)
                .ThenInclude(t => t.Position)
                .Include(t => t.PlaceType)
                .FirstOrDefaultAsync(m => m.Id == id);

            var vm = new PlaceViewModel()
            {
                Id = place.Id,
                Name = place.Name,
                City = place.City,
                Canton = place.Canton,
                Adress = place.Adress,
                PlaceTypeId = place.PlaceTypeId,
                PlaceTypes = _context.Type.ToList(),
                Contact = place.Contact,
                ContactName = place.Contact.Name,
                ContactMail = place.Contact.Mail,
                ContactPhone = place.Contact.Phone,
                ResponsibleFor = place.Contact.ResponsibleFor,
                PlaceNote = place.PlaceNote,
                PlaceConnection = place.PlaceConnection,
                PositionId = place.Contact.PositionId,
                Position = _context.Position.ToList()
            };

            if (place == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlaceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var place = await _context.Place
                        .Include(t => t.Contact)
                        .ThenInclude(t => t.Position)
                        .Include(t => t.PlaceType)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    place.Name = vm.Name;
                    place.City = vm.City;
                    place.Canton = vm.Canton;
                    place.Adress = vm.Adress;
                    place.PlaceTypeId = vm.PlaceTypeId;
                    place.Contact.Name = vm.ContactName;
                    place.Contact.Mail = vm.ContactMail;
                    place.Contact.Phone = vm.ContactPhone;
                    place.Contact.PositionId = vm.PositionId;
                    place.Contact.ResponsibleFor = vm.ResponsibleFor;
                    place.PlaceConnection = vm.PlaceConnection;
                    place.PlaceNote = vm.PlaceNote;

                    _context.Update(place);

                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Details", "Places", new { id = id });
            }

            return View(vm);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Place == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Place'  is null.");
            }
            var place = await _context.Place.FindAsync(id);

            if (place != null)
            {
                _context.Place.Remove(place);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
          return (_context.Place?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
