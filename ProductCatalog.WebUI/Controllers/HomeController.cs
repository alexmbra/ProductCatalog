using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.WebUI.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
