using EventEase_Part_1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace EventEase_Part_1.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context) { _context = context; }

        public async Task<IActionResult> Index() => View(await _context.Event.ToListAsync());
    }

}

