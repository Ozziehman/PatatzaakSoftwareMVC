using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.Data;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class VoucherStoreController : Controller
    {
        private readonly MainDb _context;

        public VoucherStoreController(MainDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("~/Views/Customer/VoucherStore.cshtml");
        }

        /// <summary>
        /// Buy a voucher with points, is triggered with AJAX from site.js
        /// </summary>
        /// <param name="voucherPercentage"></param>
        /// <param name="voucherCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult BuyVoucher(int voucherPercentage, string voucherCode, int userId)
        {
            var user = _context.users.Find(userId);

            if(user.Points < voucherPercentage)
            {
                return Json(new { success = false, message = "Not enough points", currentPoints = user.Points});
            }   

            Voucher voucher = new Voucher();
            //the discount percentage is the same as the price it costs
            voucher.Price = voucherPercentage;
            voucher.VoucherDiscount = voucherPercentage;
            voucher.VoucherCode = voucherCode;
            voucher.ExpiresBy = DateTime.Now.AddDays(7);
            voucher.UserId = userId;
            user.Points -= voucher.Price;
            _context.vouchers.Add(voucher);
            _context.SaveChanges();
            return Json(new { success = true, message = "Voucher bought", currentPoints = user.Points });
        }
    }
}
