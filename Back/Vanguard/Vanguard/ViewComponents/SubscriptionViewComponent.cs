using Microsoft.AspNetCore.Mvc;
using Vanguard.Models;

namespace Vanguard.ViewComponents
{
    public class SubscriptionViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(Subscription sub)
        {
            return View(sub);
        }
    }
}
