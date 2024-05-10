
using Microsoft.AspNetCore.Mvc;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services
{
    public interface ITagService
    {
        public Task<List<Tag>> GetAllAsync();
        public Task<IActionResult?> CreateAsync(TagCreateVM? vm);
        public Task<Tag?> GetByIdAsync(int? id);
        public Task<TagCreateVM?> EditViewAsync(int? id);
        public Task<IActionResult?> EditAsync(int? id, TagCreateVM vm);
        public Task<IActionResult?> SoftDeleteAsync(int? id);
        public Task<IActionResult?> HardDeletedAsync(int? id);
        public Task<List<Tag>> GetAllDeletedAsync();
        public Task<IActionResult?> RecoverAsync(int? id);
    }
}

