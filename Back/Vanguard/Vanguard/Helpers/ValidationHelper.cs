using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;

namespace Vanguard.Helpers;

public static class ValidationHelper
{
    public static void ValidateProduct(ProductEditVM model, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(model.Name))
            modelState.AddModelError("Name", "Product name is required.");

        if (string.IsNullOrEmpty(model.Model))
            modelState.AddModelError("Model", "Product Model is required.");


        if (string.IsNullOrEmpty(model.ShortDescription))
            modelState.AddModelError("ShortDescription", "Product Short Description is required.");

        if (model.SellPrice <= 0 || model.SellPrice == null)
            modelState.AddModelError("SellPrice", "Price must be greater than zero.");

        if (model.DiscountPrice >= model.SellPrice)
            modelState.AddModelError("DiscountPrice", "The discounted price cannot be greater than or equal to the sales price!");

        if (model.SelectedCategoryIds == null || model.SelectedCategoryIds.Length == 0)
            modelState.AddModelError("SelectedCategoryIds", "Please select at least one Category.");

        if (model.ColorSizeVM.Sizes.Count() == 0)
            modelState.AddModelError("ColorSizeVM.Sizes", "Measurement cannot be empty...!");


    }


    public static void ValidateProductCreate(ProductCreateVM model, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(model.Name))
            modelState.AddModelError("Name", "Product name is required.");

        if (string.IsNullOrEmpty(model.Model))
            modelState.AddModelError("Model", "Product Model is required.");


        if (string.IsNullOrEmpty(model.ShortDescription))
            modelState.AddModelError("ShortDescription", "Product Short Description is required.");

        if (model.SellPrice <= 0 || model.SellPrice == null)
            modelState.AddModelError("SellPrice", "Price must be greater than zero.");

        if (model.DiscountPrice >= model.SellPrice)
            modelState.AddModelError("DiscountPrice", "The discounted price cannot be greater than or equal to the sales price!");

        if (model.SelectedCategoryIds == null || model.SelectedCategoryIds.Length == 0)
            modelState.AddModelError("SelectedCategoryIds", "Please select at least one Category.");

        if ( model.ColorSizeVM==null)
        {
            modelState.AddModelError("ColorSizeVM", "Please choose color, picture and size...!");
        }
    }
}
