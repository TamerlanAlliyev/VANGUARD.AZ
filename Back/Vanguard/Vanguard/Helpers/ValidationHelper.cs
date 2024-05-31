using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.ViewModels.Account;

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

        //if (model.SelectedCategoryIds == null || model.SelectedCategoryIds.Length == 0)
        //    modelState.AddModelError("SelectedCategoryIds", "Please select at least one Category.");

        //if (model.ColorSizeVM.Sizes.Count() == 0)
        //    modelState.AddModelError("ColorSizeVM.Sizes", "Measurement cannot be empty...!");


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

        //if ( model.ColorSizeVM==null)
        //{
        //    modelState.AddModelError("ColorSizeVM", "Please choose color, picture and size...!");
        //}

        if (model.SelectedGender == null)
        {
            modelState.AddModelError("SelectedGender", "Please select Gender...!");

        }
    }

    public static void ValidateRegister(RegisterVM vm, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(vm.Name))
            modelState.AddModelError("Name", "Name is required.");

        if (string.IsNullOrEmpty(vm.Surname))
            modelState.AddModelError("Surname", "Surname is required.");



        if (string.IsNullOrEmpty(vm.Email))
            modelState.AddModelError("Email", "Email is required.");
        else if (!IsValidEmail(vm.Email))
            modelState.AddModelError("Email", "Invalid email format.");

        if (string.IsNullOrEmpty(vm.Password))
            modelState.AddModelError("Password", "Password is required.");

        if (string.IsNullOrEmpty(vm.ConfirmPassword))
            modelState.AddModelError("ConfirmPassword", "Confirm password is required.");
        else if (vm.Password != vm.ConfirmPassword)
            modelState.AddModelError("ConfirmPassword", "Password and confirm password do not match.");

        if (!string.IsNullOrEmpty(vm.PhoneNumber))
        {
            if (!IsValidPhoneNumber(vm.PhoneNumber))
                modelState.AddModelError("PhoneNumber", "Invalid phone number format.");
        }

    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+[0-9]+$");
    }

}
