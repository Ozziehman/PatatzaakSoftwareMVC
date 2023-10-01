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

        public IActionResult CompleteOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }

            order.Status = "Completed";
            _context.SaveChanges();

        
            return Json(new { success = true, message = "Complete order processed successfully", refreshPage = true });
        }
    }
}
