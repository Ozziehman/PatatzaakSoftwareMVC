using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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





        //This is for me to test if i can use models in views THIS DOES NOT WORK YET
        /* public IActionResult ItemModelTest()
         {
             ;
             return View("~/Views/DataViews/ItemModelTest.cshtml");
         }*/
        //___________________________________________________________________________






        //AL OF THESE HttpContext.Request.Form should be replaced with the use of a model, which i am still struggling with
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
            float NewPriceFloat = 0;
            float NewDiscountFloat = 0;

             //Convert everything to something the EditItemById method can use
            int itemToEditId = item.Id;
            string? NewName = item.Name;
            if (NewName.IsNullOrEmpty())
            {
                _logger.LogInformation("No new name entered");
            }
            
            float NewPrice = item.Price;
            if (NewPrice != 0 && !HttpContext.Request.Form["editPrice"].IsNullOrEmpty())
            {
                NewPriceFloat = NewPrice;
                Console.WriteLine("Succes");
            }
            else
            {
                _logger.LogInformation("No new price entered");
            }

            if (!!HttpContext.Request.Form["editDiscount"].IsNullOrEmpty())
            {
                NewDiscountFloat =  item.Discount;
            }
            else
            {
                _logger.LogInformation("No new discount entered");
            }


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
