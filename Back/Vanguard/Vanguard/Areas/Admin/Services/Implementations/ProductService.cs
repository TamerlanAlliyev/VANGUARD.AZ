using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Models;
using Vanguard.Extensions;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Data;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Helpers;
namespace Vanguard.Areas.Admin.Services.Implementations;
using Vanguard.Extensions;

public class ProductService : IProductService
{
    readonly IWebHostEnvironment _environment;
    readonly ITagService _tagService;
    readonly ICategoryService _categoryService;
    readonly VanguardContext _context;
    public ProductService(IWebHostEnvironment environment, ITagService tagService, ICategoryService categoryService, VanguardContext context)
    {
        _environment = environment;
        _tagService = tagService;
        _categoryService = categoryService;
        _context = context;
    }





    public async Task<string> RootImageCreate(IFormFile imageFile)
    {

        if (!imageFile.FileSize(5) || !imageFile.FileTypeAsync("image/"))
        {
            throw new ArgumentException("Invalid file type or size.");
        }
        if (!imageFile.FileTypeAsync("image"))
        {
            throw new ArgumentException("MainImage", "Files must be 'Image' type!");
        }
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
        var fileName = await imageFile.SaveToAsync(path);

        return fileName;
    }

    public Image ImageCreate(string ImageName, Product Product, bool IsMain, bool IsHover, Color color)
    {
        Image Image = new Image
        {
            Url = ImageName,
            IsMain = IsMain,
            IsHover = IsHover,
            Product = Product,
            Color = color
        };
        return Image;
    }



    public async Task<List<TagSelectionVM>> TagSelectionsAsync()
    {
        List<TagSelectionVM> tagSelections = new List<TagSelectionVM>();

        List<Tag> tags = await _tagService.GetAllAsync();

        foreach (var item in tags)
        {
            TagSelectionVM select = new TagSelectionVM
            {
                Id = item.Id,
                Name = item.Name
            };

            tagSelections.Add(select);
        }

        return tagSelections;
    }



    public async Task<List<CategorySelectionVM>> CategorySelectionsAsync()
    {
        List<CategorySelectionVM> categorySelections = new List<CategorySelectionVM>();

        List<Category> categories = await _categoryService.GetAllAsync();

        foreach (var cat in categories)
        {
            CategorySelectionVM select = new CategorySelectionVM
            {
                Id = cat.Id,
                Name = cat.Name,
                Parent = cat.ParentCategory?.Name
            };
            categorySelections.Add(select);
        }

        return categorySelections;
    }




    public async Task<List<TagSelectionVM>> SelectedTagAsync(int id)
    {
        List<TagSelectionVM> tagSelections = new List<TagSelectionVM>();

        List<ProductTag> productTags = await _context.ProductTag
            .Where(pt => pt.ProductId == id)
            .Include(pt => pt.Tag)
            .ToListAsync();

        foreach (var productTag in productTags)
        {
            Tag tag = productTag.Tag;
            TagSelectionVM select = new TagSelectionVM
            {
                Id = tag.Id,
                Name = tag.Name,
            };

            tagSelections.Add(select);
        }

        return tagSelections;
    }







    public async Task<List<CategorySelectionVM>> SelectedCategoryAsync(int id)
    {
        List<CategorySelectionVM> categorySelections = new List<CategorySelectionVM>();

        List<ProductCategory> productCategories = await _context.ProductCategory
            .Where(pc => pc.ProductId == id)
            .Include(pc => pc.Category)
            .ToListAsync();

        foreach (var productCategory in productCategories)
        {
            Category cat = productCategory.Category;
            CategorySelectionVM select = new CategorySelectionVM
            {
                Id = cat.Id,
                Name = cat.Name,
                Parent = cat.ParentCategory?.Name
            };
            categorySelections.Add(select);
        }

        return categorySelections;
    }









    //Edit 
    public async Task<List<SizeVM>> SelectedSizesAsync(int id)
    {
        List<SizeVM> sizeVM = new List<SizeVM>();

        var info = await _context.Informations.Where(ps => ps.ProductId == id).Include(ps => ps.Size).ToListAsync();


        if (info != null)
        {
            foreach (var item in info)
            {
                SizeVM size = new SizeVM
                {
                    Id = item.Id,
                    Weight = item.Weight,
                    Count = item.Count,
                    Dimensions = item.Dimensions,
                    Size = item.Size.Name,
                };
                sizeVM.Add(size);
            }

        }
        return sizeVM;

    }

    public async Task<ImageVM> SelectedSizesAsync(int product, int color)
    {

        List<Image> images = await _context.Images.Where(i => !i.IsDeleted && i.ProductId == product && i.ColorId == color).ToListAsync();

        ImageVM imglist = new ImageVM
        {
            MainImageURL = images.FirstOrDefault(i => i.IsMain)?.Url ?? "",
            HoverImageURL = images.FirstOrDefault(i => i.IsHover)?.Url ?? "",
            AdditionalImagesURL = images.Where(i => !i.IsHover && !i.IsMain).Select(i => i.Url).ToList(),
        };

        return imglist;
    }











    public async Task<ProductEditVM> EditViewModelAsync(int? id)
    {

        var product = await _context.Products.Where(p => !p.IsDeleted).Include(p=>p.Gender).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return null;


        List<SizeVM> sizeVM = await SelectedSizesAsync(product.Id);


        var colsiz = await _context.ProductColor.Where(pc => pc.ProductId == product.Id).Include(pc => pc.Color).FirstOrDefaultAsync();

        ImageVM imglist = await SelectedSizesAsync(product.Id, colsiz.ColorId);



        ColorSizeVM colorSize = new ColorSizeVM
        {
            ColorName = colsiz.Color.Name,
            HexCode = colsiz.Color.HexCode,
            Sizes = sizeVM,
            Images = imglist,
        };


        ProductEditVM vm = new ProductEditVM
        {
            Id = product.Id,
            Name = product.Name,
            Model = product.Model,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            SellPrice = product.SellPrice,
            DiscountPrice = product.DiscountPrice,
            CategorySelection = await CategorySelectionsAsync(),
            TagSelections = await TagSelectionsAsync(),
            SelectedTags = await _context.ProductTag.Where(pt => pt.ProductId == product.Id)
                                                    .Include(pt => pt.Tag)
                                                    .ToListAsync(),
            SelectedCategories = await _context.ProductCategory
                                               .Where(pc => pc.ProductId == product.Id)
                                               .Include(pc => pc.Category)
                                               .ToListAsync(),
            Genders = await _context.Genders.ToListAsync(),
            prodGenId = product.GenderId,
            prodGen=product.Gender.Name,
            //Colors = await _context.Colors
            //                       .Where(c => !c.IsDeleted)
            //                       .ToListAsync(),
            ColorSizeVM = colorSize,
        };

        return vm;
    }


    public async Task CategoryDeleteAsync(int productId, int catId)
    {
        var productCategoryDel = await _context.ProductCategory
            .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == catId);

        if (productCategoryDel != null)
        {
            _context.ProductCategory.Remove(productCategoryDel);
            await _context.SaveChangesAsync();
        }
    }


    public async Task TagDeleteAsync(int productId, int tagId)
    {
        var productTagDel = await _context.ProductTag
              .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.TagId == tagId);

        if (productTagDel != null)
        {
            _context.ProductTag.Remove(productTagDel);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<ServiceResult> HardDeleteAsync(int id)
    {
        Product? product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted && p.Id == id);

        List<Image> images = await _context.Images.Where(p => !p.IsDeleted && p.ProductId == id).ToListAsync();

        foreach (var image in images)
        {
            await ImageDeleteAsync(image.Url);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return ServiceResult.Ok("Product Hard deleted successfully.");
    }



    public async Task<List<ProductCategory>> ProductCategoriesCreateAsync(int productId, int[] catIds)
    {
        List<ProductCategory> productCategoriesSec = await _context.ProductCategory.Where(pc => pc.ProductId == productId).ToListAsync();
        List<ProductCategory> productCategories = new List<ProductCategory>();

        foreach (var cat in catIds)
        {
            if (!productCategoriesSec.Any(pc => pc.CategoryId == cat))
            {
                if (await _context.Categories.AnyAsync(c => c.Id == cat))
                {
                    ProductCategory productCategory = new ProductCategory
                    {
                        CategoryId = cat,
                        ProductId = productId
                    };
                    productCategories.Add(productCategory);
                }
            }
        }

        return productCategories;
    }






    public async Task<List<ProductTag>> ProductTagCreateAsync(int productId, int[] tagIds)
    {
        List<ProductTag> productTagsDel = await _context.ProductTag.Where(pt => pt.ProductId == productId).ToListAsync();
        List<ProductTag> productTags = new List<ProductTag>();


        foreach (var tag in tagIds)
        {

            if (!productTagsDel.Any(pt => pt.TagId == tag))
            {
                if (!await _context.ProductTag.AnyAsync(pt => pt.Id == tag))
                {
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tag,
                        ProductId = productId
                    };
                    productTags.Add(productTag);
                }
            }
        }

        return productTags;
    }



    public async Task<ServiceResult> ImageDeleteAsync(string url)
    {
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
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







    public async Task<ServiceResult> EditAsync(ProductEditVM vm)
    {
        var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == vm.Id);
        if (product == null) return null;

        product.Name = vm.Name;
        product.Model = vm.Model;
        product.ShortDescription = vm.ShortDescription;
        product.Description = vm.Description;
        product.SellPrice = vm.SellPrice;
        product.DiscountPrice = vm.DiscountPrice;
        product.GenderId = vm.SelectedGender;

        //Category Selects
        List<ProductCategory> productCategories = new List<ProductCategory>();
        if (vm.SelectedCategoryIds != null)
        {
            productCategories = await ProductCategoriesCreateAsync(product.Id, vm.SelectedCategoryIds);
        }

        //Tag Selects
        List<ProductTag> productTags = new List<ProductTag>();
        if (vm.SelectedTagIds != null)
        {
            productTags = await ProductTagCreateAsync(product.Id, vm.SelectedTagIds!);
        }








        List<Information> informations = new List<Information>();
        List<Size> sizes = new List<Size>();

        var colorResult = await _context.Colors.FirstOrDefaultAsync(c => c.Name == vm.ColorSizeVM.ColorName);
        if (colorResult == null)
        {
            throw new Exception("Color not found.");
        }
        Color color = colorResult;

        if (vm.ColorSizeVM.Sizes.Count() > 0)
        {
            List<Information> info = await _context.Informations
                                                   .Where(ps => ps.ProductId == product.Id)
                                                   .Include(ps => ps.Size)
                                                   .ToListAsync();

            foreach (var size in vm.ColorSizeVM.Sizes)
            {
                if (size.Size == null) return ServiceResult.BadRequest("File name is invalid.");

                var matchingInfo = info.FirstOrDefault(i => i.Id == size.Id);
                var matchingSize = await _context.Sizes.FirstOrDefaultAsync(s => s.Name == size.Size);


                if (matchingInfo != null)
                {

                    matchingInfo.Weight = size.Weight;
                    matchingInfo.Dimensions = size.Dimensions;
                    matchingInfo.Count = size.Count;

                    if (matchingSize != null)
                    {
                        matchingInfo.SizeId = matchingSize.Id;
                    }
                    else
                    {
                        Size newSize = new Size
                        {
                            Name = size.Size
                        };
                        sizes.Add(newSize);
                        matchingInfo.Size = newSize;
                    }

                }
                else
                {
                    Information inf = new Information
                    {
                        Weight = size.Weight,
                        Dimensions = size.Dimensions,
                        Count = size.Count,
                        Color = color,
                        ProductId = product.Id
                    };

                    if (matchingSize != null)
                    {
                        inf.SizeId = matchingSize.Id;
                    }
                    else
                    {
                        Size newSize = new Size
                        {
                            Name = size.Size
                        };
                        sizes.Add(newSize);
                        inf.Size = newSize;
                    }
                    informations.Add(inf);
                }
            }


        }

        //Images
        List<Image> images = new List<Image>();
        if (vm.ColorSizeVM != null && vm.ColorSizeVM.Images != null && vm.ColorSizeVM.Images.MainImage != null)
        {
            var imageDel = await _context.Images
                .Where(i => i.IsMain)
                .FirstOrDefaultAsync(i => i.ProductId == product.Id && i.ColorId == color.Id);

            if (imageDel != null)
                await ImageDeleteAsync(imageDel.Url);

            var fileName = await RootImageCreate(vm.ColorSizeVM.Images.MainImage);
            Image mainImage = ImageCreate(fileName, product, true, false, color);
            images.Add(mainImage);
        }

        if (vm.ColorSizeVM != null && vm.ColorSizeVM.Images != null && vm.ColorSizeVM.Images.HoverImage != null)
        {
            var imageDel = await _context.Images
                                         .Where(i => i.IsHover)
                                         .FirstOrDefaultAsync(i => i.ProductId == product.Id && i.ColorId == color.Id);
            if (imageDel != null)
                ImageDeleteAsync(imageDel.Url);

            var fileName = await RootImageCreate(vm.ColorSizeVM.Images.HoverImage);
            Image mainImage = ImageCreate(fileName, product, false, true, color);
            images.Add(mainImage);
        }


        if (vm.ColorSizeVM != null && vm.ColorSizeVM.Images != null && vm.ColorSizeVM.Images.AdditionalImages != null)
        {
            foreach (var AdditionalImages in vm.ColorSizeVM.Images.AdditionalImages)
            {
                var fileName = await RootImageCreate(AdditionalImages);
                Image mainImage = ImageCreate(fileName, product, false, false, color);
                images.Add(mainImage);
            }

        }



        await _context.Images.AddRangeAsync(images);
        await _context.Sizes.AddRangeAsync(sizes);
        await _context.Informations.AddRangeAsync(informations);
        await _context.ProductTag.AddRangeAsync(productTags);
        await _context.ProductCategory.AddRangeAsync(productCategories);

        await _context.SaveChangesAsync();

        return ServiceResult.Ok("Product Edit successfully.");
    }










    public async Task<ServiceResult> CreateAsync(ProductCreateVM vm)
    {

        List<Color> colors = new List<Color>();
        List<Product> products = new List<Product>();
        List<Image> images = new List<Image>();
        List<Size> sizes = new List<Size>();
        List<Information> informations = new List<Information>();
        List<ProductCategory> productCategories = new List<ProductCategory>();
        List<ProductTag> productTags = new List<ProductTag>();
        List<ProductColor> productColors = new List<ProductColor>();


        foreach (var item in vm.ColorSizeVM)
        {
            //Color Create
            Color color = new Color();
            if (_context.Colors.Where(c => !c.IsDeleted).Any(c => c.HexCode == item.HexCode))
            {
                var col = await _context.Colors.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.HexCode == item.HexCode);
                if (col != null)
                {
                    color = col;
                }
            }
            else
            {
                color = new Color
                {
                    Name = item.ColorName,
                    HexCode = item.HexCode,
                };
                colors.Add(color);
            }

            //Product Create
            Product product = new Product
            {
                Name = vm.Name,
                Model = vm.Model,
                ShortDescription = vm.ShortDescription,
                Description = vm.Description,
                SellPrice = vm.SellPrice,
                DiscountPrice = vm.DiscountPrice,
                GenderId = vm.SelectedGender
            };
            products.Add(product);


            //ProductColor Create
            ProductColor productColor = new ProductColor
            {
                Product = product,
                Color = color
            };
            productColors.Add(productColor);




            //Category Selects
            if (vm.SelectedCategoryIds != null)
            {
                foreach (var cat in vm.SelectedCategoryIds)
                {
                    ProductCategory productCategory = new ProductCategory
                    {
                        CategoryId = cat,
                        Product = product
                    };
                    productCategories.Add(productCategory);
                }
            }


            //Tag Selects
            if (vm.SelectedTagIds != null)
            {
                foreach (var tag in vm.SelectedTagIds)
                {
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tag,
                        Product = product
                    };
                    productTags.Add(productTag);
                }
            }





            //Images Create
            if (item.Images.MainImage != null)
            {
                if (color != null)
                {
                    var fileName = await RootImageCreate(item.Images.MainImage);
                    Image mainImage = ImageCreate(fileName, product, true, false, color);
                    images.Add(mainImage);
                }
            }

            if (item.Images.HoverImage != null)
            {
                if (color != null)
                {
                    var fileName = await RootImageCreate(item.Images.HoverImage);
                    Image hoverImage = ImageCreate(fileName, product, false, true, color);
                    images.Add(hoverImage);
                }
            }

            if (item.Images.AdditionalImages != null)
            {
                foreach (var additionalImage in item.Images.AdditionalImages)
                {
                    if (color != null)
                    {
                        var fileName = await RootImageCreate(additionalImage);
                        Image additionalImg = ImageCreate(fileName, product, false, false, color);
                        images.Add(additionalImg);
                    }
                }
            }




            //Sizes Create

            if (item.Sizes != null)
            {
                foreach (var siz in item.Sizes)
                {
                    Size size = new Size();
                    Information info = new Information();

                    if (await _context.Sizes.Where(s => !s.IsDeleted).AnyAsync(s => s.Name == siz.Size))
                    {

                        size = await _context.Sizes.Where(s => !s.IsDeleted).FirstOrDefaultAsync(s => s.Name == siz.Size);
                        info = new Information
                        {
                            Product = product,
                            Color = color,
                            SizeId = size.Id,
                            Count = siz.Count,
                            Dimensions = siz.Dimensions,
                            Weight = siz.Weight
                        };
                        informations.Add(info);
                    }
                    else if (sizes.Any(s => s.Name == siz.Size))
                    {
                        size = sizes.FirstOrDefault(s => s.Name == siz.Size);
                        info = new Information
                        {
                            Product = product,
                            Color = color,
                            Size = size,
                            Count = siz.Count,
                            Dimensions = siz.Dimensions,
                            Weight = siz.Weight
                        };
                        informations.Add(info);
                    }
                    else
                    {
                        size = new Size
                        {
                            Name = siz.Size,
                        };
                        sizes.Add(size);

                        info = new Information
                        {
                            Product = product,
                            Color = color,
                            Size = size,
                            Count = siz.Count,
                            Dimensions = siz.Dimensions,
                            Weight = siz.Weight
                        };
                        informations.Add(info);
                    }


                }
            }



        }




        await _context.Colors.AddRangeAsync(colors);
        await _context.Products.AddRangeAsync(products);
        await _context.Images.AddRangeAsync(images);
        await _context.Sizes.AddRangeAsync(sizes);
        await _context.Informations.AddRangeAsync(informations);
        await _context.ProductCategory.AddRangeAsync(productCategories);
        await _context.ProductTag.AddRangeAsync(productTags);
        await _context.ProductColor.AddRangeAsync(productColors);

        await _context.SaveChangesAsync();


        return ServiceResult.Ok("Product Creat successfully.");
    }










    public async Task<Product?> ProductGetDetailAsync(int? id)
    {

        Product? product = await _context.Products.Include(p => p.Images)
                                                 .Include(p => p.ProductColors)
                                                    .ThenInclude(pc => pc.Color)
                                                 .Include(i => i.Images)
                                                 .Include(g=>g.Gender)
                                                 .Include(i => i.Information)
                                                    .ThenInclude(s => s.Size)
                                                 .Include(p => p.ProductCategory)
                                                    .ThenInclude(p => p.Category)
                                                 .Include(p => p.ProductTag)
                                                    .ThenInclude(p => p.Tag)
                                                 .FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }


    public async Task<ProductDetailVM> DetailAsync(int? id)
    {
        if (id == null || id <= 1) ServiceResult.BadRequest("Invalid product ID.");

        Product? product = await ProductGetDetailAsync(id);

        if (product == null) ServiceResult.NotFound("Product not found.");

        var color = product!.ProductColors.Color;
        var images = product!.Images;
        var info = product!.Information;
        var categories = product.ProductCategory.Select(pc => pc.Category.Name).ToList();
        var tags = product.ProductTag.Select(pc => pc.Tag.Name).ToList();
        int totalCount = 0;
        bool InStock = false;
        foreach (var item in info)
        {
            totalCount += item.Count;
        }

        if (totalCount > 0) InStock = true;

        decimal? discountPercentage = null;
        if (product.DiscountPrice != null && product.DiscountPrice > 0 && product.SellPrice > 0)
        {
            discountPercentage = ((decimal)(product.SellPrice - product.DiscountPrice) / (decimal)product.SellPrice) * 100;
        }

        ProductDetailVM vm = new ProductDetailVM
        {
            Product = product,
            Offer = (int?)discountPercentage,
            Color = color.Name,
            HexCode = color.HexCode,
            MainImageURL = images!.FirstOrDefault(i => i.IsMain)!.Url,
            HoverImageURL = images!.FirstOrDefault(i => i.IsHover)!.Url,
            AdditionalImagesURL = images.Where(i => !i.IsMain && !i.IsHover).Select(i => i.Url).ToList(),
            Informations = info,
            InStock = InStock,
            Categories = categories,
            Tags = tags
        };
        return vm;
    }


    public async Task<ServiceResult> InformationDeleteAsync(int? id)
    {
        if (id == null || id < 1) return ServiceResult.BadRequest("Invalid ID");

        Information? info = await _context.Informations.FirstOrDefaultAsync(p => p.Id == id);
        if (info == null) return ServiceResult.NotFound("Information not found");

        _context.Informations.Remove(info);
        await _context.SaveChangesAsync();

        return ServiceResult.Ok("Information deleted successfully");
    }


}













