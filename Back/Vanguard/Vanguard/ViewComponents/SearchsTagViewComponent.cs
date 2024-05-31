using Microsoft.AspNetCore.Mvc;

namespace Vanguard.ViewComponents;
[ViewComponent]
public class SearchsTagViewComponent:ViewComponent
{
    public IViewComponentResult Invoke() {  return View(); }
}
