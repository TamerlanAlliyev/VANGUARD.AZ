using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.ViewModels.About;

namespace Vanguard.Controller
{
    public class AboutController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly VanguardContext _context;

        public AboutController(VanguardContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM vm = new AboutVM
            {
                About = await _context!.About!.Include(x => x.Image)!.FirstOrDefaultAsync()!,
                AboutAccordions = await _context.AboutAccordion.ToListAsync(),
                Emploees = await _context.AboutEmploees.Include(e=>e.Image).ToListAsync(),
                Contact = await _context.Contacts!.FirstOrDefaultAsync()!
            };

            return View(vm);
        }
    }
}
