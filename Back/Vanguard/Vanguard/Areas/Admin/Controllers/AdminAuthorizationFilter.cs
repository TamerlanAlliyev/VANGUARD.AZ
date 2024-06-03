using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YourNamespace.Filters
{
    public class AdminAuthorizationFilter : IAsyncAuthorizationFilter 
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context) 
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("AccessCode", "Account", new { area = "Admin" });
                return; 
            }
          

            if (context.HttpContext.User.IsInRole("Customer"))
            {
                context.Result = new RedirectToActionResult("Forbidden", "Home", new { area = "" });
                return;
            }
        }
    }
}
