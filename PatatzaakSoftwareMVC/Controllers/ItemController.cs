using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
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












        //UPDATE
        private void EditItem(string itemToEditId)
        {
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
        }

        public IActionResult ProcessEditForm()
        {
            string itemToEditId = HttpContext.Request.Form["ItemToEditId"];
            EditItem(itemToEditId);
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }



        //DELETE
    }
}
