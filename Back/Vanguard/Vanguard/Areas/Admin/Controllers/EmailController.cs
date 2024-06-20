using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.Email;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Data;
using Vanguard.Helpers;

namespace Vanguard.Areas.Admin.Controllers;
[Area("Admin")]
public class EmailController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly IEmailService _emailService;
    readonly IWebHostEnvironment _environment;
    public EmailController(VanguardContext context, IEmailService emailService, IWebHostEnvironment environment)
    {
        _context = context;
        _emailService = emailService;
        _environment = environment;
    }

    public async Task<IActionResult> Inbox()
    {

        try
        {
            var inbox = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).ToListAsync();
            return View(inbox);

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

    public async Task<IActionResult> Compose()
    {
        try
        {

            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View();

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









    [HttpPost]
    public async Task<IActionResult> Compose(ComposeVM vm)
    {
        try
        {
            List<string> EmailList = new List<string>();

            switch (vm.SendingMethod)
            {
                case 1:
                    EmailList = await _context.Subscriptions
                                              .Where(subscr => subscr.Email != null)
                                              .Select(subscr => subscr.Email!)
                                              .ToListAsync();
                    break;
                case 2:
                    EmailList = await _context.Users
                                              .Where(user => user.Email != null)
                                              .Select(user => user.Email!)
                                              .ToListAsync();
                    break;
                case 3:
                    var subscriptionEmails = await _context.Subscriptions
                                                           .Where(subscr => subscr.Email != null)
                                                           .Select(subscr => subscr.Email!)
                                                           .ToListAsync();

                    var userEmails = await _context.Users
                                                   .Where(user => user.Email != null)
                                                   .Select(user => user.Email!)
                                                   .ToListAsync();

                    EmailList.AddRange(subscriptionEmails);
                    EmailList.AddRange(userEmails);
                    break;
                case 4:
                    EmailList.Add(vm.Email!);
                    break;
                default:
                    break;
            }

            string? filePath = null;
            if (vm.File != null)
            {
              
                var uploads = Path.Combine(_environment.WebRootPath, "client", "assets", "emailfiles");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var fileName = Path.GetFileName(vm.File.FileName);
                filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.File.CopyToAsync(stream);
                }
            }

            foreach (var email in EmailList)
            {
                if (filePath != null)
                {
                    _emailService.Send(email, vm.Subject, $"<p>{vm.Body}</p>", true, filePath);
                }
                else
                {
                    _emailService.Send(email, vm.Subject, $"<p>{vm.Body}</p>", true);
                }
            }

            return RedirectToAction("Inbox");

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









    public async Task<IActionResult> Send()
    {
        try
        {
            var inbox = await _context.Connections.Where(c => !c.IsDeleted && c.IsSend).ToListAsync();
            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View(inbox);

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


    public async Task<IActionResult> SendRead(int id)
    {
        try
        {
            var mes = await _context.Connections.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            mes.IsRead = true;
            await _context.SaveChangesAsync();
            SendEmailVM Email = new SendEmailVM
            {
                Message = mes,
            };
            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View(Email);
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

    public async Task<IActionResult> Read(int id)
    {
        try
        {
            var mes = await _context.Connections.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            mes.IsRead = true;
            await _context.SaveChangesAsync();
            SendEmailVM Email = new SendEmailVM
            {
                Message = mes,
            };
            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View(Email);
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


    [HttpPost]
    public async Task<IActionResult> SendEmail(SendEmailVM vm)
    {
        try
        {
            var mes = await _context.Connections.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == vm.Message.Id);

            _emailService.Send(vm.Message.Email, "Reply to your message", $"<p  >{vm.Email}</p>", true);

            mes.IsSend = true;
            mes.SendEmal = vm.Email;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inbox));
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


    [HttpPost]
    public async Task<IActionResult> ReadAll(int[] ids)
    {
        try
        {
            var messages = _context.Connections.Where(c => !c.IsDeleted).AsQueryable();

            foreach (var num in ids)
            {
                var mes = await messages.FirstOrDefaultAsync(mes => mes.Id == num);
                if (mes != null)
                {
                    mes.IsRead = true;
                }
            }
            await _context.SaveChangesAsync();

            return Ok();

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

    [HttpPost]
    public async Task<IActionResult> EmailDelete(int[] ids)
    {
        try
        {
            var messages = _context.Connections.Where(c => !c.IsDeleted).AsQueryable();

            foreach (var num in ids)
            {
                var mes = await messages.FirstOrDefaultAsync(mes => mes.Id == num);
                _context.Connections.Remove(mes);
            }
            await _context.SaveChangesAsync();

            return Ok();


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

    [HttpPost]
    public async Task<IActionResult> EmailDeleteGet(int id)
    {
        try
        {
            var messages = await _context.Connections.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            _context.Connections.Remove(messages);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inbox));
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
