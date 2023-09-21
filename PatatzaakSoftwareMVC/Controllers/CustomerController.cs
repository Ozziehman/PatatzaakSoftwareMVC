using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CRUDItemPage()
        {
            return View("~/Views/Customer/CRUDItemPage.cshtml");
        }

        [HttpPost]
        public IActionResult FillOrderWith(int itemId, int orderId)
        {
            OrderedItem orderedItem = new OrderedItem().ConvertItemToOrderedItem(itemId, orderId);
            if (new OrderedItem().ConvertItemToOrderedItem(itemId, orderId) != null) 
            {
                _logger.LogInformation($"orderedItem {orderedItem.Id} with ItemName {orderedItem.Item.Name} added to order with orderId: {orderedItem.Order.Id}");
            }               

            //Clicking the Order Button should make the order defifinitive and display the complete prices. All the orderedItems that have: orderedItem.Order.Id == orderId can be looked up using a database query. 
            //So if You want all the orderedItem(.Item) that belong to a specific order you should do a   dbContext.Find.Where(orderedItem.Order.Id == orderId)

            _logger.LogInformation($"filling order with item {itemId} on order {orderId}");
            return Json(new { success = true, message = "item added" });
        }
    }
}
