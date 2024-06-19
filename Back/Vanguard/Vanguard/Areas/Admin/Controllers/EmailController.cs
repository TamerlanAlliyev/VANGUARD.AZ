using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Data;
using Vanguard.Helpers;

namespace Vanguard.Areas.Admin.Controllers;
[Area("Admin")]
public class EmailController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;

    public EmailController(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Inbox()
    {

        try
        {
            var inbox = await _context.Connections.Where(c => !c.IsDeleted).ToListAsync();

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

    public async Task<IActionResult> Read(int id)
    {

        try
        {
            var mes = await _context.Connections.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            mes.IsRead = true;
            await _context.SaveChangesAsync();

            return View(mes);

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
