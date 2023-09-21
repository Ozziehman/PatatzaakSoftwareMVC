using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CustomerController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CRUDItemPage()
        {
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

        //Testing with AJAX
        /* private List<int> receiptItems = new List<int>();


         [HttpPost]
         public ActionResult AddToReceipt(int itemId)
         {
             // Add the itemId to your C# list.
             receiptItems.Add(itemId);

             // You can perform any additional processing here if needed.
             foreach(int item in receiptItems)
             {
                 Console.WriteLine(item);
             }   
             return Json(new { success = true }); // Send a JSON response to the client.
         }*/
    }
}
