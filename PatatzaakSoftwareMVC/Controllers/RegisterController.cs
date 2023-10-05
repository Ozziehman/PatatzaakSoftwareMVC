using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MainDb _context;

        public RegisterController(MainDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// In production there should be at least 1 admin account already made to be able to 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (!_context.users.Any(u => u.Email == user.Email))
                {
                    user.Points = 0;
                    user.IsAdmin = false;
                    // Add the user to the context
                    _context.Add(user);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.errorMessage = "Email already exists. Please choose a different email.";
                    return View("Index");
                }

            }
            ViewBag.errorMessage = "User registration failed! Please check input fields for";
            return View("Index");
        }
    }
}
