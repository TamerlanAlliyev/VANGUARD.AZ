using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;

namespace Vanguard.Controller
{
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly VanguardContext _context;

        public ContactController(VanguardContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.FirstOrDefaultAsync());
        }
    }
}
