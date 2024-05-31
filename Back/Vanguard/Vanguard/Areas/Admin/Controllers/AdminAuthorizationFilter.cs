using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YourNamespace.Filters
{
    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
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
