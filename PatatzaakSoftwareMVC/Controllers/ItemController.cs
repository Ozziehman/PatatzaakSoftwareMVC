using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;


namespace PatatzaakSoftwareMVC.Controllers

{
    //_____________________________________________________________________________
    //This is the controller controller i made myself to understand MVC better     |  
    //_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_|
    //I later figured out i used the DbContext in a inconvenient way so the plan is|
    //To switch to a different method that works with the generated CRUD page from EF
    //This would be a way friendlier development environment                       | 
    //_____________________________________________________________________________|


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


        //CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(Item item)
        {
            

            if (new Item().CreateItem(item.Name, item.Price, item.Discount) > 0)
            {
                _logger.LogInformation("Item created");
            }
            else
            {
                _logger.LogInformation("Item not created");
            }
            return View("~/Views/ItemCRUD/CRUDItemPage.cshtml");
        }

        //READ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessLoadForm(Item item)
        {
            int itemToLoadId = item.Id;
            return View("~/Views/DataViews/ItemJsonDataView.cshtml", new Item().LoadItemById(itemToLoadId));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessEditForm(Item item)
        {
            float NewPriceFloat = item.Price;
            float NewDiscountFloat = item.Discount;
            int itemToEditId = item.Id;
            string? NewName = item.Name;
        
            if (new Item().EditItemById(itemToEditId, NewName, NewPriceFloat, NewDiscountFloat) > 0)
            {
                _logger.LogInformation("item found and edited");
            }
            else
            {
                _logger.LogInformation("item not found/edit failed/No actual change entered");
            }
                
            return View("~/Views/ItemCRUD/CRUDItemPage.cshtml");
        }

        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessDeletionForm(Item item)
        {
            int itemToDeleteId = item.Id;

            if (new Item().DeleteItemById(itemToDeleteId) > 0)
            {
                _logger.LogInformation("item found and deleted");
            }
            else
            {
                _logger.LogInformation("item not found/delete failed");
            }
            return View("~/Views/ItemCRUD/CRUDItemPage.cshtml");
        }

    }
}
