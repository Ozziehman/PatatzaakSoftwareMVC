using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Data;
using Newtonsoft.Json;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Models.ViewModels;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly MainDb _context;
        private readonly ILogger<OrderHistoryController> _logger;

        // Constructor that combines both parameters
        public OrderHistoryController(MainDb context, ILogger<OrderHistoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Makes viewbag for items to be displayed
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            User currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            ViewBag.Orders = _context.orders.Where(o => o.UserId == currentUser.Id && o.Status == "Completed").ToList();
            ViewBag.OrdersBeingMade = _context.orders.Where(o => o.UserId == currentUser.Id && o.Status == "Placed").ToList();
            return View("~/Views/Customer/OrderHistory.cshtml", new OrderHistoryViewModel(_context));
        }

        /// <summary>
        /// reverts ordr to placed to reorder
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult RevertToPlaced(int orderId)
        {
            var orderToReorder = _context.orders.Find(orderId);
            orderToReorder.Status = "Placed";
            orderToReorder.Finished = false;
            orderToReorder.TimePlaced = DateTime.Now;
            if (_context.SaveChanges() > 0)
            {
                _logger.LogInformation("Order reordered");
                return Json(new { success = true, message = $"Succesfully replaced order", refreshPage = true });
            }
            else
            {
                _logger.LogInformation("Order not reordered");
                return Json(new { success = false, message = $"Failed to replace order" });
            }
        }
    }
}
