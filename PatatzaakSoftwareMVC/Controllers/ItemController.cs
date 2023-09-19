using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.PageModels;

namespace PatatzaakSoftwareMVC.Controllers

{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("~/Views/Company/CRUDItemPage.cshtml");
        }

        //This is for me to test if i can use models in views
        public IActionResult ItemModelTest()
        {
            ;
            return View("~/Views/DataViews/ItemModelTest.cshtml");
        }

        //CREATE
        public IActionResult CreateItem()
        {
            Item item = new Item()
            {
                //Data should be entered into here, maybe use HttpContext.Request.Form[""] to get data from form
                Name= "TestItem",
                Price = 1.00f,
                Discount = 0.00f,
                ImagePath = "../Resources/Images/placeholder.jpg"
            };
 
            if (item.CreateItem(item) > 0)
            {
                _logger.LogInformation("Item created");
            }
            else
            {
                _logger.LogInformation("Item not created");
            }
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

        //READ
        public IActionResult ProcessLoadForm()
        {
            string itemToLoadId = HttpContext.Request.Form["ItemToLoadId"];
            return View("~/Views/DataViews/ItemJsonDataView.cshtml", new Item().LoadItemById(int.Parse(itemToLoadId)));
        }
        
        public IActionResult LoadAllItemsAsJson()
        {           
            return View("~/Views/DataViews/ItemJsonDataView.cshtml", new Item().LoadItems());
        }

        public IActionResult LoadItemsAsObjects()
        {        
            return View("~/Views/DataViews/ItemObjectDataView.cshtml", new Item().LoadItemsObject());      
        }

        //UPDATE
        public IActionResult ProcessEditForm()
        {
            string itemToEditId = HttpContext.Request.Form["ItemToEditId"];
            if (new Item().EditItemById(int.Parse(itemToEditId)) > 0)
            {
                _logger.LogInformation("item found and edited");
            }
            else
            {
                _logger.LogInformation("item not found/edit failed/No actual change entered");
            }
                
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

        //DELETE
        public IActionResult ProcessDeletionForm()
        {
            string itemToDeleteId = HttpContext.Request.Form["ItemToDeleteId"];

            if (new Item().DeleteItemById(int.Parse(itemToDeleteId)) > 0)
            {
                _logger.LogInformation("item found and deleted");
            }
            else
            {
                _logger.LogInformation("item not found/delete failed");
            }
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

    }
}
