using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

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
            return View("~/Views/DataViews/ItemModelTest.cshtml");
        }
        //CREATE
        public IActionResult CreateItem(string Name, float Price, float Discount)
        {
            using (var dbContext = new MainDb())
            {
                var item = new Item
                {
                    Name = "testItem",
                    Price = 1.00f,
                    Discount = 0.00f
                };

                dbContext.item.Add(item);
                dbContext.SaveChanges();
            }
            _logger.LogInformation("Item created");
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }


        //READ
        public IActionResult ProcessLoadForm()
        {
            string itemToLoadId = HttpContext.Request.Form["ItemToLoadId"];

            using (var dbContext = new MainDb())
            {
                var itemToLoad = dbContext.item.Where(item => item.Id == int.Parse(itemToLoadId)).FirstOrDefault();

                if (itemToLoad != null)
                {
                    _logger.LogInformation("Item loaded");
                    var jsonItem = JsonConvert.SerializeObject(itemToLoad, Formatting.Indented); // Convert to json
                    return View("~/Views/DataViews/ItemJsonDataView.cshtml", jsonItem); // Go to new page wit json data
                }
                else
                {
                    _logger.LogInformation("Item not found");
                    return View("~/Views/Customer/CRUDItemPage.cshtml");
                }
            }
        }

        public IActionResult LoadAllItemsAsJson()
        {
            using (var dbContext = new MainDb())
            {
                var itemsToLoad = dbContext.item.ToList(); // Load all items

                if (itemsToLoad.Count > 0)
                {
                    _logger.LogInformation("Items loaded");
                    var jsonItems = JsonConvert.SerializeObject(itemsToLoad, Formatting.Indented); // Convert to JSON
                    return View("~/Views/DataViews/ItemJsonDataView.cshtml", jsonItems); // Go to new page with JSON data
                }
                else
                {
                    _logger.LogInformation("No items found");
                    return View("~/Views/Customer/CRUDItemPage.cshtml");
                }
            }
        }



        //UPDATE
        public IActionResult ProcessEditForm()
        {
            string itemToEditId = HttpContext.Request.Form["ItemToEditId"];

            using (var dbContext = new MainDb())
            {
                var itemToEdit = dbContext.item.Where(item => item.Id == int.Parse(itemToEditId)).FirstOrDefault();

                if (itemToEdit != null)
                {
                    //THIS IS AN EXAMPLE EDIT
                    itemToEdit.Name = "EditedItem";

                    dbContext.SaveChanges();
                    _logger.LogInformation("Item edited");
                }
                else
                {
                    _logger.LogInformation("Item not found");
                }
            }
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }



        //DELETE
        public IActionResult ProcessDeletionForm()
        {
            string itemToDeleteId = HttpContext.Request.Form["ItemToDeleteId"];

            using (var dbContext = new MainDb())
            {
                var itemToDelete = dbContext.item.Where(item => item.Id == int.Parse(itemToDeleteId)).FirstOrDefault();

                if (itemToDelete != null)
                {
                    //THIS IS AN EXAMPLE EDIT
                    dbContext.item.Remove(itemToDelete);

                    dbContext.SaveChanges();
                    _logger.LogInformation("Deleted");
                }
                else
                {
                    _logger.LogInformation("Item not found");
                }
            }
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

    }
}
