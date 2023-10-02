using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;
using static PatatzaakSoftwareMVC.Models.Order;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MainDb _context;
        private readonly ILogger<CustomerController> _logger;
        public int sessionOrderId;
        

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

            return View("~/Views/Customer/CustomerPage.cshtml", new CustomerPageViewModel(_context));

        }
        

        [HttpPost]
        public IActionResult FillOrderWith(int itemId, int orderId)
        {
            sessionOrderId = orderId;
            _logger.LogInformation($"SESSION ORDER ID: {sessionOrderId}");
            OrderedItem newOrderedItem = new OrderedItem();
            newOrderedItem.Item = _context.items.Find(itemId); // Assign the associated Item entity
            newOrderedItem.Order = _context.orders.Find(orderId); // Assign the associated Order entity
            
            // Add to the context and save changes
            _context.orderedItems.Add(newOrderedItem);
            int result = _context.SaveChanges();

            //Clicking the Order Button should make the order defifinitive and display the complete prices. All the orderedItems that have: orderedItem.Order.Id == orderId can be looked up using a database query. 
            //So if You want all the orderedItem(.Item) that belong to a specific order you should do a   _context.Where(oi => oi.Order.Id == orderId)
            if (result > 0)
            {
                _logger.LogInformation($"filling order with item {newOrderedItem.Item.Id} on order with order id: {newOrderedItem.Order.Id}");
            }
            else
            {
                _logger.LogInformation($"Failed!");
            }
         
            return Json(new { success = true, message = "item added" });
        }

        public IActionResult PlaceOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            order.Status = "Placed";
            float totalPrice= 0;
            List<OrderedItem> orderedItemsInOrder = _context.orderedItems.Where(o => o.OrderId == orderId).ToList();
           
            int result = _context.SaveChanges();
            if (result > 0)
            {

                List<object> orderedItemInfoList = new List<object>();

                foreach (OrderedItem orderedItem in orderedItemsInOrder)
                {
                    var item = _context.items.Find(orderedItem.ItemId);
                    //add total price to the order to make it easier to know what the client has to pay
                    totalPrice += (float)Math.Round(item.Price - (item.Price * (item.Discount / 100)), 2);
                    
                    orderedItemInfoList.Add(new //object
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                    });
                }
                order.TotalPrice = (float)Math.Round((totalPrice), 2);
                _context.SaveChanges();


                var response = new
                {
                    success = true,
                    message = $"Order placed and order status of order with Id {orderId} changed to 'Placed'",
                    orderedItems = orderedItemInfoList,
                };

                return Json(response);

                //Need to go to a confirmation page after this, that confirms your order is placed
            }
            else
            {
                _logger.LogInformation($"Failed");
                return Json(new { success = false, message = $"Failed to place order OR order is duplicate" });
            }
        }
    }
}
