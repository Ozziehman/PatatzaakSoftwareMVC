using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models.DataAccess;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddItem()
        {
            _logger.LogInformation("AddItem action called");

            _logger.LogInformation(ItemDataAccess.AddItem());

            _logger.LogInformation("AddItem method from ItemDataAccess called");

            return View("~/Views/Home/CustomerPage.cshtml");
        }
    }
}