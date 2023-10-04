using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Controllers
{

    
    public class LoginController : Controller
    {

        private readonly MainDb _context;
        private readonly ILogger<LoginController> _logger;


        public LoginController(MainDb context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Checks if user credentials are in database, then logs in storing the user in the sessionstorage
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult Login(User user)
        {
            if (IsValidUser(user.Email, user.Password))
            {
                var sessionUser = GetUser(user.Email);


                var userJson = JsonConvert.SerializeObject(sessionUser);
                //store user into sessionstorage
                HttpContext.Session.SetString("User", userJson);
                
                _logger.LogInformation($"Session user name: {HttpContext.Session.GetString("User")}");
                return RedirectToAction("Index","Home");
            }
            else
            {
                var validationMessage = "Invalid credentials";
                ViewBag.ErrorMessage = validationMessage;
                return View("~/Views/Login/Index.cshtml");
            }       
        }


        /// <summary>
        /// Validates user credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValidUser(string email, string password)
        {
            //validate if user credentials match an existing user
            bool isValidUser = _context.users.Any(u => u.Email == email && u.Password == password);

            return isValidUser;
        }



        /// <summary>
        /// returns full user object to store in the session storage based on the email which is entered
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUser(string email)
        {
            // get full user to store in sessionstorage
            var sessionUser = _context.users.Where(u => u.Email == email).FirstOrDefault();
            return sessionUser;
        }
    }
}
