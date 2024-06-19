using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vanguard.Areas.Admin.ViewModels.About;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;
using YourNamespace.Filters;

namespace Vanguard.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class AboutController : Microsoft.AspNetCore.Mvc.Controller
{
	readonly VanguardContext _context;
	readonly IWebHostEnvironment _environment;
	public AboutController(VanguardContext context, IWebHostEnvironment environment)
	{
		_context = context;
		_environment = environment;
	}

	public async Task<IActionResult> Title()
	{
		About about = new About();
		about = await _context.About.Include(a => a.Image)!.FirstOrDefaultAsync()!;

		return View(about);
	}

	[HttpPost]
	public async Task<IActionResult> Title(About about)
	{
		//About about = new About();
		var exsistAbout = await _context.About.Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == about.Id);
		exsistAbout.Title = about.Title;
		if (about.ImageFile != null)
		{
			Image Image = new Image();
			var root = Path.Combine("cilent", "assets", "settings", "about", "title");
			var path = Path.Combine(_environment.WebRootPath, root);
			if (exsistAbout.Image.Url != null)
				await ImageDeleteAsync(exsistAbout.Image.Url, root);

			if (!about.ImageFile.FileSize(5) || !about.ImageFile.FileTypeAsync("image/"))
			{
				ModelState.AddModelError("ProfilImage", "Invalid file type or size.");
				return View(about);
			}
			if (!about.ImageFile.FileTypeAsync("image"))
			{
				ModelState.AddModelError("ProfilImage", "Files must be 'image' type!.");
				return View(about);
			}


			var filename = await about.ImageFile.SaveToAsync(path);

			Image = new Image
			{
				Url = filename,
				About = exsistAbout,
			};
			exsistAbout.Image = Image;
			await _context.Images.AddAsync(Image);

		}

		await _context.SaveChangesAsync();
		return View(exsistAbout);
	}


	public async Task<ServiceResult> ImageDeleteAsync(string url, string root)
	{
		var path = Path.Combine(_environment.WebRootPath, root);
		Image image = await _context.Images.FirstOrDefaultAsync(i => i.Url == url);
		if (image == null) ServiceResult.NotFound("Image not found.");

		string fileName = image.Url;
		if (string.IsNullOrEmpty(fileName)) ServiceResult.BadRequest("File name is invalid.");

		try
		{
			ImageFileExtension.DeleteImagesService(path, fileName);
			_context.Images.Remove(image);
			await _context.SaveChangesAsync();

			return ServiceResult.Ok("Image deleted successfully.");
		}
		catch (Exception ex)
		{
			return ServiceResult.InternalServerError($"Internal server error: {ex.Message}");
		}
	}


	public async Task<IActionResult> Accordion()
	{
		var accordions = await _context.AboutAccordion.ToListAsync();
		return View(accordions);
	}

	public IActionResult AccordionCreate()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> AccordionCreate(AboutAccordion accordion)
	{
		if (!ModelState.IsValid)
		{
			return View(accordion);
		}
		AboutAccordion aboutAccordion = new AboutAccordion
		{
			Title = accordion.Title,
			Description = accordion.Description,
		};

		await _context.AboutAccordion.AddAsync(aboutAccordion);
		await _context.SaveChangesAsync();
		return RedirectToAction("Accordion");
	}

	public async Task<IActionResult> AccordionDelete(int id)
	{
		var acc = await _context.AboutAccordion.FirstOrDefaultAsync(a => a.Id == id);
		_context.AboutAccordion.Remove(acc);
		await _context.SaveChangesAsync();
		return Ok();
	}

	public async Task<IActionResult> AccordionEdit(int id)
	{
		var exsistAccordion = await _context.AboutAccordion.FirstOrDefaultAsync(a => a.Id == id);

		return View(exsistAccordion);
	}

	[HttpPost]
	public async Task<IActionResult> AccordionEdit(AboutAccordion accordion)
	{
		if (!ModelState.IsValid)
		{
			return View(accordion);
		}

		var exsistAccordion = await _context.AboutAccordion.FirstOrDefaultAsync(a => a.Id == accordion.Id);

		if (exsistAccordion == null)
			return NotFound(exsistAccordion);

		exsistAccordion.Title = accordion.Title;
		exsistAccordion.Description = accordion.Description;

		await _context.SaveChangesAsync();
		return RedirectToAction("Accordion");
	}







	public async Task<IActionResult> Emploee()
	{
		List<AboutEmploees> emploees = new List<AboutEmploees>();
		emploees = await _context.AboutEmploees.Include(e => e.Image).ToListAsync();
		return View(emploees);
	}

	public IActionResult EmploeeCreate()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> EmploeeCreate(AboutEmploeeCreateVM vm)
	{
		if (!ModelState.IsValid)
		{
			return View(vm);
		}

		AboutEmploees emploee = new AboutEmploees
		{
			FullName = vm.Fullname,
			Position = vm.Position,
		};

		if (vm.ImageFile != null)
		{
			Image Image = new Image();
			var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "about", "emploee");



			if (!vm.ImageFile.FileSize(5) || !vm.ImageFile.FileTypeAsync("image/"))
			{
				ModelState.AddModelError("ProfilImage", "Invalid file type or size.");
				return View(vm);
			}
			if (!vm.ImageFile.FileTypeAsync("image"))
			{
				ModelState.AddModelError("ProfilImage", "Files must be 'image' type!.");
				return View(vm);
			}


			var filename = await vm.ImageFile.SaveToAsync(path);

			Image = new Image
			{
				Url = filename,
				AboutEmploees = emploee,
			};
			emploee.Image = Image;
			await _context.Images.AddAsync(Image);
		}

		await _context.AboutEmploees.AddAsync(emploee);
		await _context.SaveChangesAsync();
		return RedirectToAction("Emploee");
	}


	[HttpPost]
	public async Task<IActionResult> EmploeeDelete(int id)
	{
		var exsistEmploee = await _context.AboutEmploees.Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == id);

		var root = Path.Combine("cilent", "assets", "settings", "about", "emploee");
		await ImageDeleteAsync(exsistEmploee.Image.Url, root);

		_context.AboutEmploees.Remove(exsistEmploee);
		await _context.SaveChangesAsync();

		return Ok();
	}








	public async Task<IActionResult> EmploeeEdit(int id)
	{
		var emp = await _context.AboutEmploees.Include(e => e.Image).FirstOrDefaultAsync(x => x.Id == id);
		if (emp == null)
		{
			return NotFound();
		}

		AboutEmploeeCreateVM vm = new AboutEmploeeCreateVM
		{
			Id = id,
			Fullname = emp.FullName,
			Position = emp.Position,
			Image = emp.Image.Url
		};

		return View(vm);
	}

	[HttpPost]
	public async Task<IActionResult> EmploeeEdit(AboutEmploeeCreateVM vm)
	{
		var emp = await _context.AboutEmploees.Include(e => e.Image).FirstOrDefaultAsync(x => x.Id == vm.Id);
		if (emp == null)
		{
			return NotFound();
		}

		emp.FullName = vm.Fullname;
		emp.Position = vm.Position;

		if (vm.ImageFile != null)
		{
			var root = Path.Combine("cilent", "assets", "settings", "about", "emploee");
			var path = Path.Combine(_environment.WebRootPath, root);

			if (!vm.ImageFile.FileSize(5) || !vm.ImageFile.FileTypeAsync("image/"))
			{
				ModelState.AddModelError("ProfilImage", "Invalid file type or size.");
				return View(vm);
			}

			var filename = await vm.ImageFile.SaveToAsync(path);


			if (!string.IsNullOrEmpty(emp.Image?.Url))
			{
				await ImageDeleteAsync(emp.Image.Url, root);
			}

			var newImage = new Image
			{
				Url = filename,
				AboutEmploees = emp
			};

			emp.Image = newImage;
			await _context.Images.AddAsync(newImage);
		}

		await _context.SaveChangesAsync();

		return RedirectToAction("Emploee");
	}



	public async Task<IActionResult> Contact()
	{
		if (await _context.Contacts.FirstOrDefaultAsync()==null)
		{
			Contact contact = new Contact
			{
				Address = "Port Baku Mall, 151 Neftchilar Avenue, Baku",
				AddressUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48613.5977531735!2d49.819509220986006!3d40.40109968039558!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40307d6bd6211cf9%3A0x343f6b5e7ae56c6b!2zQmFrxLE!5e0!3m2!1saz!2saz!4v1714147771064!5m2!1saz!2saz",
				AddressLink = "maps.app.goo.gl/WWYfJmJJxduwx3Ne9",
				Number= "0507101015",
				Email= "vanguardfashionaz@gmail.com"
            };

			await _context.Contacts.AddAsync(contact);
			await _context.SaveChangesAsync();
		}
		return View(await _context.Contacts.FirstOrDefaultAsync());
	}

	[HttpPost]
    public async Task<IActionResult> Contact(Contact con)
    {
		if (!ModelState.IsValid)
		{
			return View(con);
		}
		var contact = await _context.Contacts.FirstOrDefaultAsync();
        contact.Address= con.Address;
        contact.AddressLink= con.AddressLink;
        contact.AddressUrl=con.AddressUrl;
        contact.Number = con.Number;
        contact.Email = con.Email;
		 _context.Contacts.Update(contact);
		await _context.SaveChangesAsync();
        return View();
    }
}