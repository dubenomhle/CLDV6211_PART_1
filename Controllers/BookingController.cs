using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventEase_Part_1.Data;
using EventEase_Part_1.Models;

namespace EventEase_Part_1.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string searchTerm, int? eventTypeId, DateTime? startDate, DateTime? endDate, bool? onlyAvailable)
        {
            ViewBag.EventTypes = new SelectList(await _context.EventType.ToListAsync(), "EventTypeID", "Name");

            var bookings = from b in _context.Booking
                           .Include(b => b.Venue)
                           .Include(b => b.Event)
                           .ThenInclude(e => e.EventType)
                           select b;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookings = bookings.Where(b =>
                    b.BookingID.ToString().Contains(searchTerm) ||
                    b.Event.EventName.Contains(searchTerm));
            }

            if (eventTypeId.HasValue)
            {
                bookings = bookings.Where(b => b.Event.EventTypeID == eventTypeId);
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate <= endDate.Value);
            }

            if (onlyAvailable.HasValue && onlyAvailable.Value)
            {
                bookings = bookings.Where(b => b.Venue.Availability);
            }

            return View(await bookings.ToListAsync());
        }


        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewBag.VenueID = new SelectList(_context.Venue.ToList(), "VenueID", "VenueName");
            ViewBag.EventID = new SelectList(_context.Event.ToList(), "EventID", "EventName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,EventID,VenueID,BookingDate")] Booking booking)
        {
            Console.WriteLine("POST /Booking/Create hit!");

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is INVALID");

            ViewBag.VenueID = new SelectList(_context.Venue, "VenueID", "VenueName", booking.VenueID);
            ViewBag.EventID = new SelectList(_context.Event, "EventID", "EventName", booking.EventID);
            return View(booking);
        }



        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
