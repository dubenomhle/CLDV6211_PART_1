using EventEase_Part_1.Data;
using EventEase_Part_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase_Part_1.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        // ✅ REPLACED OLD INDEX METHOD WITH FILTERING ENHANCED VERSION – Part 3 Question Step 7a
        public async Task<IActionResult> Index(string searchType, int? venueId, DateTime? startDate, DateTime? endDate)
        {
            var events = _context.Event
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchType))
            {
                events = events.Where(e => e.EventType.Name == searchType);
            }

            if (venueId.HasValue)
            {
                events = events.Where(e => e.VenueID == venueId);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                events = events.Where(e => e.EventDate >= startDate && e.EventDate <= endDate);
            }

            // ✅ Provide data for dropdown filters in the View
            ViewData["EventTypes"] = _context.EventType.ToList();
            ViewData["Venues"] = _context.Venue.ToList();

            return View(await events.ToListAsync());
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewData["Venues"] = _context.Venue.ToList();

            // Part 3 Question (Step 5) ✅ ADDED: Load event types for dropdown
            ViewData["EventTypes"] = _context.EventType.ToList();

            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();

            // Part 3 Question (Step 5) ✅ ADDED: Reload event types on failed form submit
            ViewData["EventTypes"] = _context.EventType.ToList();

            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["Venues"] = _context.Venue.ToList();

            // Part 3 Question (Step 5) ADDED: Load event types for dropdown
            ViewData["EventTypes"] = _context.EventType.ToList();

            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.EventID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();

            // Part 3 Question (Step 5) ADDED: Reload event types on failed edit submit
            ViewData["EventTypes"] = _context.EventType.ToList();

            return View(@event);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null) return NotFound();

            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null) return NotFound();

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventID == id);
        }
    }
}
