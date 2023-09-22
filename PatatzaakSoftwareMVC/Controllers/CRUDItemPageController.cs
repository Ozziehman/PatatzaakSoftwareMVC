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

        public IActionResult Create()
        {
            return View("~/Views/ItemCRUD/Create.cshtml");
        }

        public IActionResult Read()
        {
            return View("~/Views/ItemCRUD/Read.cshtml");
        }
        public IActionResult Update()
        {
            return View("~/Views/ItemCRUD/Update.cshtml");
        }
        public IActionResult Delete()
        {
            return View("~/Views/ItemCRUD/Delete.cshtml");
        }

    }
}
