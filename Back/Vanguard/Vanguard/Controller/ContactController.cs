using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.Helpers;
using Vanguard.Models;
using Vanguard.ViewModels.Contact;

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
            ContactVM contactVM = new ContactVM
            {
                Contact = await _context.Contacts.FirstOrDefaultAsync(),
            };

            contactVM.CustomerConnection = new Connection();

            return View(contactVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactVM vm)
        {
           

            ModelState.Clear();
            ValidationHelper.ValidateConnect(vm, ModelState);

            if (!ModelState.IsValid)
            {
                vm.Contact = await _context.Contacts.FirstOrDefaultAsync();
                return View(vm);

            }

            var customerConnection = new Connection
            {
                Name = vm.CustomerConnection.Name,
                Email = vm.CustomerConnection.Email,
                Message = vm.CustomerConnection.Message,
            };
            await _context.Connections.AddAsync(customerConnection);
            await _context.SaveChangesAsync();


            ContactVM contactVM = new ContactVM
            {
                Contact = await _context.Contacts.FirstOrDefaultAsync(),
                CustomerConnection = new Connection()
            };

            return View(contactVM);
        }

    
    }
}
