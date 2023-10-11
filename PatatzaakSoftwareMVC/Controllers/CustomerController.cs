using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            var userJson = HttpContext.Session.GetString("User");
            var sessionUser = JsonConvert.DeserializeObject<User>(userJson);
            var items = _context.items.ToList();
            ViewBag.Items = items;

            var userVouchers = _context.vouchers.Where(v => v.UserId == sessionUser.Id).ToList();
            ViewData["UserVoucherList"] = new SelectList(userVouchers, "Id", "VoucherDisplay");

            return View("~/Views/Customer/CustomerPage.cshtml", new CustomerPageViewModel(_context));
        }
        

        /// <summary>
        /// Is activated through AJAX from the site.js script to fill the order with an item, 
        /// it creates an orderedItem that is linked to an item and and order. 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
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


        /// <summary>
        /// is activated through AJAX from the site.js script to place the order and make it visible to the company 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IActionResult PlaceOrder(int orderId, string voucherId)
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

                if (voucherId != null)
                {
                    Voucher voucher = _context.vouchers.Find(int.Parse(voucherId));
                    if (voucher != null)
                    {
                        if (voucher.ExpiresBy > DateTime.Now)
                        {
                            totalPrice = (float)Math.Round(totalPrice - (totalPrice * (voucher.VoucherDiscount / 100)), 2);
                            _context.vouchers.Remove(voucher);         
                        }
                    }
                }

                
                order.TotalPrice = (float)Math.Round((totalPrice), 2);

                //give the user points for each full euro they spent
                var user = _context.users.Find(order.UserId);

                user.Points += (int)Math.Round(totalPrice);
                

                _context.SaveChanges();

                var response = new
                {
                    success = true,
                    message = $"Order placed and order status of order with Id {orderId} changed to 'Placed'",
                    orderedItems = orderedItemInfoList,
                    currentPoints = user.Points
                };

                return Json(response);

           
            }
            else
            {
                _logger.LogInformation($"Failed");
                return Json(new { success = false, message = $"Failed to place order OR order is duplicate" });
            }
        }

        public IActionResult ClearOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            var orderedItems = _context.orderedItems.Where(o => o.OrderId == orderId).ToList();
            foreach (var orderedItem in orderedItems)
            {
                _context.orderedItems.Remove(orderedItem);
            }
            _context.SaveChanges();
            return Json(new { success = true, message = "Order cleared" });
        }   
    }
}
