using Microsoft.AspNetCore.Mvc;

namespace Vanguard.ViewComponents
{
    public class SearchTagViewComponent: ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return View();
        }
    }
}
