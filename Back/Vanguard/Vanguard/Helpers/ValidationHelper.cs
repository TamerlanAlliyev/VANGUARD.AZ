using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using Vanguard.Areas.Admin.ViewModels.Blog;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.ViewModels.Account;
using Vanguard.ViewModels.Setting;

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

    public static void ValidateUserInfoUpdate(UserInfoUpdate model, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(model.Name))
            modelState.AddModelError("Name", "Name is required.");

        if (string.IsNullOrEmpty(model.SurName))
            modelState.AddModelError("SurName", "Surname is required.");

        if (string.IsNullOrEmpty(model.Email))
            modelState.AddModelError("Email", "Email is required.");
        else if (!IsValidEmail(model.Email))
            modelState.AddModelError("Email", "Invalid email format.");

        if (!string.IsNullOrEmpty(model.Phone))
        {
            if (!IsValidPhoneNumber(model.Phone))
                modelState.AddModelError("Phone", "Invalid phone number format.");
        }

        if (!string.IsNullOrEmpty(model.Postal))
        {
            if (!IsValidPostalCode(model.Postal))
                modelState.AddModelError("Postal", "Invalid postal code format.");
        }
    }



    public static void ValidateBlogCreate(BlogCreateVM model, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(model.Title))
            modelState.AddModelError("Title", "Blog title is required.");

        if (string.IsNullOrEmpty(model.MainDescription))
            modelState.AddModelError("MainDescription", "Main Description is required.");

        if (model.MainFile == null)
            modelState.AddModelError("MainFile", "Main File is required.");
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



    private static bool IsValidPostalCode(string postalCode)
    {
        return Regex.IsMatch(postalCode, @"^\d{5}(-\d{4})?$");
    }


}
