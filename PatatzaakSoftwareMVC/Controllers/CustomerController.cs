using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MainDb _context;
        private readonly ILogger<CustomerController> _logger;

        // Constructor that combines both parameters
        public CustomerController(MainDb context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var items = _context.items.ToList();
            ViewBag.Items = items;

            return View("~/Views/Customer/CustomerPage.cshtml", new CustomerPageViewModel());

        }

        [HttpPost]
        public IActionResult FillOrderWith(int itemId, int orderId)
        {

            OrderedItem newOrderedItem = new OrderedItem();
            newOrderedItem.Item = _context.items.Find(itemId); // Assign the associated Item entity
            ; // Assign the associated Order entity

            // Add to the context and save changes
            _context.orderedItems.Add(newOrderedItem);
            int result = _context.SaveChanges();

            //Clicking the Order Button should make the order defifinitive and display the complete prices. All the orderedItems that have: orderedItem.Order.Id == orderId can be looked up using a database query. 
            //So if You want all the orderedItem(.Item) that belong to a specific order you should do a   _context.Where(oi => oi.Order.Id == orderId)
            if (result > 0)
            {
                _logger.LogInformation($"filling order with item {itemId} on order with order id: {orderId}");
            }
            else
            {
                _logger.LogInformation($"Failed!");
            }
         
            return Json(new { success = true, message = "item added" });
        }
    }
}
