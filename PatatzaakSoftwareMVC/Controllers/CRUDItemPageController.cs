using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CRUDItemPageController : Controller
    {

        private readonly ILogger<CRUDItemPageController> _logger;

        public CRUDItemPageController(ILogger<CRUDItemPageController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
