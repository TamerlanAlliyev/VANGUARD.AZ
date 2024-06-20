using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.Email;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Data;
using Vanguard.Helpers;
using Vanguard.Models;

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

			string? SendingMet = null;

			switch (vm.SendingMethod)
			{
				case 1:
					EmailList = await _context.Subscriptions
											  .Where(subscr => subscr.Email != null)
											  .Select(subscr => subscr.Email!)
											  .ToListAsync();
					SendingMet = "Subscriptions";

					break;
				case 2:
					EmailList = await _context.Users
											  .Where(user => user.Email != null)
											  .Select(user => user.Email!)
											  .ToListAsync();
					SendingMet = "Customers";

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
					SendingMet = "Subscriptions and Customers";

					break;
				case 4:
					EmailList.Add(vm.Email!);
					SendingMet = "Personnel";

					break;
				default:
					break;
			}

			string? filePath = null;
			string? fileNam = null;

			if (vm.File != null)
			{

				var uploads = Path.Combine(_environment.WebRootPath, "client", "assets", "emailfiles");
				if (!Directory.Exists(uploads))
				{
					Directory.CreateDirectory(uploads);
				}

				var fileName = Path.GetFileName(vm.File.FileName);
				filePath = Path.Combine(uploads, fileName);
				fileNam = fileName;


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


			EmailSend emailSend = new EmailSend
			{
				Subject = vm.Subject,
				Body = vm.Body,
				FileUrl = fileNam,
				SendingMethod = SendingMet!

			};

			if (vm.File!=null)
			{

            switch (vm.File.ContentType)
            {
                case var type when type.StartsWith("image"):
                    emailSend.FileType = "image";
                    break;
                case var type when type.StartsWith("video"):
                    emailSend.FileType = "video";
                    break; 
                case var type when type.StartsWith("application/pdf"):
                    emailSend.FileType = "pdf";
                    break;
                default:
                    break;
            }
            }

            if (SendingMet == "Personnel" || SendingMet == "")
			{
				emailSend.Email = vm.Email;
			}

			await _context.EmailSends.AddAsync(emailSend);
			await _context.SaveChangesAsync();

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
            var sends = await _context.EmailSends.Where(c => !c.IsDeleted).ToListAsync();
            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View(sends);

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
            var sends = await _context.EmailSends.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            await _context.SaveChangesAsync();
          
            ViewData["InboxCount"] = await _context.Connections.Where(c => !c.IsDeleted && !c.IsSend).CountAsync();

            return View(sends);
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
    public async Task<IActionResult> SendDelete(int[] ids)
    {
        try
        {
            var Sends = _context.EmailSends.Where(c => !c.IsDeleted).AsQueryable();

            foreach (var num in ids)
            {
                var send = await Sends.FirstOrDefaultAsync(mes => mes.Id == num);
                _context.EmailSends.Remove(send);
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
    public async Task<IActionResult> SendDeleteGet(int id)
    {
        try
        {
            var send = await _context.EmailSends.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            _context.EmailSends.Remove(send);
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






































    public async Task<IActionResult> Reply()
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


	public async Task<IActionResult> ReplyRead(int id)
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

    //Methods
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
