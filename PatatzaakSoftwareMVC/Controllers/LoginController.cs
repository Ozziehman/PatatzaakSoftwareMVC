using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Controllers
{

    
    public class LoginController : Controller
    {

        private readonly MainDb _context;
        private readonly ILogger<LoginController> _logger;
        public const string SessionKeyName = "_Name";
        public const string SessionKeyEmail = "_Email";

        public LoginController(MainDb context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(User user)
        {
            if (IsValidUser(user.Email, user.Password))
            {
                var sessionUser = GetUser(user.Email);

                var sessionUserEmail = sessionUser.Email;
                var sessionUserName = sessionUser.Name;
                var sessionUserPoints = sessionUser.Points;

                //store user into sessionstorage
                HttpContext.Session.SetString(SessionKeyName, sessionUserName);
                HttpContext.Session.SetString(SessionKeyEmail, sessionUserEmail);

                _logger.LogInformation($"Session user name: {HttpContext.Session.GetString(SessionKeyName)}");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var validationMessage = "Invalid credentials";
                ViewBag.ErrorMessage = validationMessage;
                return View("~/Views/Login/Index.cshtml");
            }       
        }

        public bool IsValidUser(string email, string password)
        {
            //validate if user credentials match an existing user
            bool isValidUser = _context.users.Any(u => u.Email == email && u.Password == password);

            return isValidUser;
        }

        public User GetUser(string email)
        {
            // get full user to store in sessionstorage
            var sessionUser = _context.users.Where(u => u.Email == email).FirstOrDefault();
            return sessionUser;
        }
    }
}
