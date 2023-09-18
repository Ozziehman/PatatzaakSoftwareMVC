using Microsoft.AspNetCore.Mvc;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class NavigationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerPage()
        {
            return View("~/Views/Customer/CustomerPage.cshtml");
        }

        public IActionResult CompanyPage()
        {
            return View("~/Views/Company/CompanyPage.cshtml");
        }

        public IActionResult CRUDItemPage() 
        {
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }
    }
}
