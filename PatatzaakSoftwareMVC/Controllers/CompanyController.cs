using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly MainDb _context;

        public CompanyController(MainDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("~/Views/Company/CompanyPage.cshtml", new CompanyPageViewModel(_context));
        }
    }
}
