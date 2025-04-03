using EventEase_Part_1.Data;
using EventEase_Part_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace EventEase_Part_1.Controllers
{
    public class VenueController : Controller
{
    private readonly ApplicationDbContext _context;

    public VenueController(ApplicationDbContext context) { _context = context; }

    public async Task<IActionResult> Index() => View(await _context.Venue.ToListAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Venue venue)
    {
        if (ModelState.IsValid)
        {
            _context.Add(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(venue);
    }
}
}


