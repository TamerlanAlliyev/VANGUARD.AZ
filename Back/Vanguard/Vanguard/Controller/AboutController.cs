using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.Helpers;
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
            try
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
            catch (KeyNotFoundException ex)
            {
                return View("Error404", new ServiceResult(false, ex.Message, 404));
            }
            catch (ArgumentException ex)
            {
                return View("Error400", new ServiceResult(false, ex.Message, 400));
            }
            catch (UnauthorizedAccessException ex)
            {
                return View("Error401", new ServiceResult(false, ex.Message, 401));
            }
            catch (Exception ex)
            {
                return View("Error500", new ServiceResult(false, ex.Message, 500));
            }


        }


    }
}
