﻿using Microsoft.AspNetCore.Mvc;
using YourNamespace.Filters;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]

public class HomeController : Microsoft.AspNetCore.Mvc.Controller 
{
    public IActionResult Index()
    {
        return View();
    }
}
