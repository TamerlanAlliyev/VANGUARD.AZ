using Microsoft.EntityFrameworkCore;
using System.Linq;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Implementations
{
	public class SettingHomeHeroService: ISettingHomeHeroService
	{
		private readonly VanguardContext _context;

		public SettingHomeHeroService(VanguardContext context)
		{
			_context = context;
		}

		public async Task<SettingHomeHero> GetOrCreateSettingHomeHeroAsync()
		{
			var settingHomeHero = await _context.SettingHomeHero.FirstOrDefaultAsync();
			if (settingHomeHero == null)
			{
				settingHomeHero = new SettingHomeHero();
				await _context.SettingHomeHero.AddAsync(settingHomeHero);
				await _context.SaveChangesAsync();
			}
			return settingHomeHero;
		}

		public async Task UpdateSettingHomeHeroAsync(SettingHomeHero updatedModel)
		{
			var settingHomeHero = await _context.SettingHomeHero.FirstOrDefaultAsync();
			if (settingHomeHero != null)
			{
				settingHomeHero.Offer = updatedModel.Offer;
				settingHomeHero.HeroName = updatedModel.HeroName;
				settingHomeHero.Title = updatedModel.Title;
				settingHomeHero.Description = updatedModel.Description;
				settingHomeHero.Time = updatedModel.Time;
				settingHomeHero.CategoryId = updatedModel.CategoryId;
				settingHomeHero.TagId = updatedModel.TagId;

				await _context.SaveChangesAsync();
			}
		}
	}
}
