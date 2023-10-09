using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models.ViewModels;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly MainDb _context;

        public CompanyController(MainDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("~/Views/Company/CompanyPage.cshtml", new CompanyPageViewModel(_context));
        }


        /// <summary>
        /// Changes clicked order to a "Ready" status, is triggered with AJAX from site.js
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult ReadyOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }

            order.Status = "Ready";
            _context.SaveChanges();

            return Json(new { success = true, message = "Ready order processed successfully", refreshPage = true });
        }


        /// <summary>
        /// Changes clicked order to a "Completed" status, is triggered with AJAX from site.js
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult CompleteOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }
            //give the user points for each full euro they spent
            var user = _context.users.Find(order.UserId);

            user.Points += (int)Math.Round(order.TotalPrice);

            order.Status = "Completed";
            order.Finished = true;
            _context.SaveChanges();

        
            return Json(new { success = true, message = "Complete order processed successfully", refreshPage = true });
        }
    }
}
