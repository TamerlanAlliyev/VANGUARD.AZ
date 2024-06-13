//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Vanguard.Data;
//using Vanguard.Models;
//using Vanguard.ViewModels.Blog;
//using Vanguard.ViewModels.Shop;

//namespace Vanguard.ViewComponents
//{
//    public class BlogListViewComponent : ViewComponent
//    {
//        readonly VanguardContext _context;
//        public BlogListViewComponent(VanguardContext context)
//        {
//            _context = context;
//        }

      



//        public async Task<IViewComponentResult> InvokeAsync(BlogViewVM postMV, int[]? tagId)
//        {
//            var query = _context.Blogs
//                .Where(b => !b.IsDeleted)
//                .Include(b => b.Images)
//                .Include(b => b.AppUser)
//                .ThenInclude(b => b.AllowedEmployee!.Role)
//                .Include(b => b.Categories)
//                .ThenInclude(bc => bc.Category)
//                .Include(b => b.BlogTags).AsQueryable();

//            if (tagId != null && tagId.Length > 0)
//            {
//                query = query.Where(b => b.BlogTags.Any(bt => tagId.Contains(bt.TagId)));
//            }

//            var blogs = await query.ToListAsync();

//            var categories = blogs
//                .SelectMany(b => b.Categories)
//                .Select(bc => bc.Category)
//                .Distinct()
//                .ToList();

//            var popularBlogs = blogs
//                .Where(b => !b.IsDeleted)
//                .OrderByDescending(b => b.Clickeds)
//                .Take(5)
//                .Select(b => new BlogAllVM
//                {
//                    Id = b.Id,
//                    Title = b.Title,
//                    Clicked = b.Clickeds,
//                    Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
//                    IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
//                    Created = b.CreatedDate,
//                    CreatedBy = b.AppUser.FullName!,
//                    Author = b.AppUser.FullName!,
//                }).ToList();

//            if (postMV.SelectedCategory > 0)
//            {
//                blogs = blogs
//                    .Where(b => b.Categories.Any(c => c.CategoryId == postMV.SelectedCategory))
//                    .ToList();
//            }

//            int totalItems = blogs.Count;

//            var pageSize = 9;
//            var pageNumber = postMV.PageInfo?.CurrentPage ?? 1;

//            var pagedBlogs = blogs
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToList();

//            var blogVMs = pagedBlogs.Select(b => new BlogAllVM
//            {
//                Id = b.Id,
//                Title = b.Title,
//                Clicked = b.Clickeds,
//                Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
//                IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
//                Created = b.CreatedDate,
//                CreatedBy = b.AppUser.FullName!,
//            }).ToList();

//            BlogViewVM vm = new BlogViewVM
//            {
//                Blogs = blogVMs,
//                Categories = categories,
//                SelectedCategory = postMV.SelectedCategory,
//                PopularBlogs = popularBlogs,
//                PageInfo = new PageInfo
//                {
//                    CurrentPage = pageNumber,
//                    ItemsPerPage = pageSize,
//                    TotalItems = totalItems
//                }
//            };

//            return View(vm);
//        }
//    }
//}
