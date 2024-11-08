using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab5.Models;

namespace Lab5.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}





















/*using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [Route("/profile")]
        public IActionResult Profile()
        {
            var userProfile = new UserViewModel
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                FullName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            };
            return View(userProfile);
        }




        AuthManagements authManagements = new AuthManagements();
        [HttpPost]
        public async Task<IActionResult> RegistrationAuth0(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    ViewBag.Error = "Паролі не збігаються!";
                    return View("Index");
                }
                string clientToken = await authManagements.GetClientTokenAsync();
                await authManagements.CreateUserAsync(username, fullname, password, phone, email, clientToken);
                ViewBag.Message = "Користувача успішно створено!";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginAuth0(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Введіть логін та пароль!";
                    return View("Index");
                }
                string userToken = await authManagements.GetUserTokenAsync(username, password);

                Response.Cookies.Append("AuthToken", userToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });

                return Redirect("/Control");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }



        *//*        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
                public IActionResult Error()
                {
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }*//*
    }
}
*/