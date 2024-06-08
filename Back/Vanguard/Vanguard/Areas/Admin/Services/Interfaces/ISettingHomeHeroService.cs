using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface ISettingHomeHeroService
{
	public Task<SettingHomeHero> GetOrCreateSettingHomeHeroAsync();
	public Task UpdateSettingHomeHeroAsync(SettingHomeHero updatedModel);

}
