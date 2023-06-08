using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpringOutreach.Data;
using SpringOutreach.Models;
using SpringOutreach.ViewModels;

namespace SpringOutreach.Controllers
{
    //[Authorize]
    public class OutreachesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Random random = new Random();

        public OutreachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Outreaches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Outreach.Include(o => o.Place);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Outreaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Outreach == null)
            {
                return NotFound();
            }

            var outreach = await _context.Outreach
                .Include(o => o.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outreach == null)
            {
                return NotFound();
            }

            return View(outreach);
        }

        // GET: Outreaches/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new OutreachViewModel()
            {
                PlaceId = id,
                Place = _context.Place.FirstOrDefault(t => t.Id == id),
                Statuses = _context.Status
                .ToList()
            };

            return View(vm);
        }

        // POST: Outreaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, OutreachViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var stringId = RandomString(12);
                var outreach = new Outreach()
                {
                    InternContact = vm.InternContact,
                    InternResponsible = vm.InternResponsible,
                    StringId = stringId,
                    Note = vm.Note,
                    Year = vm.Year,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    PlaceId = id,
                    StatusId = vm.StatusId
                };

                _context.Add(outreach);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Places", new { id = id });
            }

            return RedirectToAction("Details", "Places", new {id = id});
        }

        // GET: Outreaches/Edit/5
        public async Task<IActionResult> Edit(int? id, int? placeId)
        {
            if (id == null || _context.Outreach == null)
            {
                return NotFound();
            }

            var outreach = await _context.Outreach
                .FirstOrDefaultAsync(t => t.Id == id);

            var vm = new OutreachViewModel()
            {
                Id = outreach.Id,
                Place = _context.Place.FirstOrDefault(t => t.Id == placeId),
                PlaceId = placeId,
                InternContact = outreach.InternContact,
                InternResponsible = outreach.InternResponsible,
                Note = outreach.Note,
                Year = outreach.Year,
                StartDate = outreach.StartDate,
                EndDate = outreach.EndDate,
                StatusId = outreach.StatusId,
                Statuses = _context.Status.ToList()
            };

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Outreaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? placeId, OutreachViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var outreach = await _context.Outreach
                        .Include(t => t.PdfFile)
                        .Include(t => t.Place)
                        .Include(t => t.Status)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    outreach.InternContact = vm.InternContact;
                    outreach.InternResponsible = vm.InternResponsible;
                    outreach.Year = vm.Year;
                    outreach.Note = vm.Note;
                    outreach.StartDate = vm.StartDate;
                    outreach.EndDate = vm.EndDate;
                    outreach.StatusId = vm.StatusId;

                    _context.Update(outreach);
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

        // GET: Outreaches/Delete/5
        public async Task<IActionResult> Delete(int? id, int? placeId)
        {
            if (id == null || _context.Outreach == null)
            {
                return NotFound();
            }

            var outreach = await _context.Outreach
                .Include(o => o.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outreach == null)
            {
                return NotFound();
            }

            return View(outreach);
        }

        // POST: Outreaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Outreach == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OutreachYear'  is null.");
            }
            var outreach = await _context.Outreach
                .Include(o => o.Place)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (outreach != null)
            {
                _context.Outreach.Remove(outreach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Places", new { id = outreach.Place.Id });
        }

        private bool OutreachExists(int id)
        {
          return (_context.Outreach?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Random String Method
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // GET: Outreaches/Upload
        public async Task<IActionResult> Upload(int? id)
        {
            var vm = new OutreachViewModel()
            {
                OutreachId = id
            };

            return View(vm);
        }

        //Upload PDF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(OutreachViewModel vm)
        {
            if (ModelState.IsValid)
            {
                
                if (vm.File == null)
                {
                    return View(vm);
                }

                byte[] fileBytes;
                string fileName;

                using (var ms = new MemoryStream())
                {                
                    vm.File.CopyTo(ms);
                    fileBytes = ms.ToArray();
                    fileName = Path.GetFileName(vm.File.FileName);
                }

                var pdfFile = new PdfFile()
                {
                    FileBytes = fileBytes,
                    FileName = fileName,
                    OutreachId = vm.Id
                };

                await _context.PdfFile.AddAsync(pdfFile);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Places");
        }

            vm.File = null;
            return RedirectToAction("Index", "Places");
    }

        //Download PDF
        [HttpGet, ActionName("Download")]
        public async Task<FileResult> DownloadFileAsync(int fileId)
        {
            var pdf = await _context.PdfFile
                .FirstOrDefaultAsync(x => x.Id == fileId);

            return File(pdf.FileBytes, "application/pdf+zip", pdf.FileName);
        }

        //Return and open PDF
        [HttpGet, ActionName("Content")]
        public async Task<FileResult> GetFileAsync(int fileId)
        {
            var pdf = await _context.PdfFile
                .FirstOrDefaultAsync(x => x.Id == fileId);

            return new FileContentResult(pdf.FileBytes, MediaTypeNames.Application.Pdf);
        }

        //Delete PDF
        [HttpGet, ActionName("DeletePdf")]
        public async Task<IActionResult> DeleteFileAsync(int fileId, int? id)
        {
            var pdf = await _context.PdfFile.FindAsync(fileId);

            if (pdf != null)
            {
                _context.PdfFile.Remove(pdf);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Places", new { id = id });
        }
    }
}
