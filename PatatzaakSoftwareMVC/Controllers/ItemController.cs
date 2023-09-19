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
        public IActionResult ItemModelTest()
        {
            ;
            return View("~/Views/DataViews/ItemModelTest.cshtml");
        }
        //___________________________________________________________________________


        




        //CREATE
        public IActionResult CreateItem()
        {
            string Name = HttpContext.Request.Form["ItemName"];
            float Price= float.Parse(HttpContext.Request.Form["ItemPrice"]);
            float Discount = float.Parse(HttpContext.Request.Form["ItemDiscount"]);


            if (new Item().CreateItem(Name, Price, Discount) > 0)
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
            float NewPriceFloat = 0;
            float NewDiscountFloat = 0;

             //Convert everything to something the EditItemById method can use
            int itemToEditId = int.Parse(HttpContext.Request.Form["ItemToEditId"]);
            string? NewName = HttpContext.Request.Form["ItemNewName"];
            if (NewName.IsNullOrEmpty())
            {
                _logger.LogInformation("No new name entered");
            }
            string? NewPrice = HttpContext.Request.Form["ItemNewPrice"];
            if (!NewPrice.IsNullOrEmpty())
            {
                NewPriceFloat = float.Parse(NewPrice);
            }
            else
            {
                _logger.LogInformation("No new price entered");
            }

            string? NewDiscount = HttpContext.Request.Form["ItemNewDiscount"];
            if (!NewDiscount.IsNullOrEmpty())
            {
                NewDiscountFloat = float.Parse(NewDiscount);
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
