using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab6.ViewModels;
using System.Security.Claims;
using Lab6.Services;

namespace Lab6.Controllers;

public class AccountController(Auth0UserService auth0UserService) : Controller
{
    private readonly Auth0UserService _auth0UserService = auth0UserService;

    [HttpGet]
    [Route("/account/register")]
    public IActionResult Register()
    {
        return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
    }

    [HttpPost]
    [Route("/account/register")]
    public async Task<IActionResult> Register(UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _auth0UserService.CreateUserAsync(model);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    [Route("/account/login")]
    public IActionResult Login()
    {
        return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
    }


    [HttpPost]
    [Route("/account/login")]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var token = await _auth0UserService.AuthenticateUserAsync(model);

            UserProfileViewModel userProfile = await _auth0UserService.GetUserInfo(token);
            List<Claim> claims =
            [
                    new Claim(ClaimTypes.NameIdentifier, userProfile.Email),
                    new Claim(ClaimTypes.Name, userProfile.FullName),
                    new Claim(ClaimTypes.Email, userProfile.Email),
                    new Claim("ProfileImage", userProfile.ProfileImage),
                    new Claim(ClaimTypes.MobilePhone, userProfile.PhoneNumber),
                    new Claim("Username", userProfile.Username),
            ];

            var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("AuthScheme", claimsPrincipal);

            StoreToken(token);

            return RedirectToAction("Profile", "Account");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error authenticating user: {ex.Message}");
            return View(model);
        }
    }

    [Authorize]
    [Route("/account/profile")]
    public IActionResult Profile()
    {
        string alternativeValue = "N/A";
        ClaimsPrincipal user = HttpContext.User;

        UserProfileViewModel profileViewModel = new()
        {
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? alternativeValue,
            FullName = user.FindFirst(ClaimTypes.Name)?.Value ?? alternativeValue,
            PhoneNumber = user.FindFirst(ClaimTypes.MobilePhone)?.Value ?? alternativeValue,
            ProfileImage = user.FindFirst("ProfileImage")?.Value ?? alternativeValue,
            Username = user.FindFirst("Username")?.Value ?? alternativeValue
        };

        return View(profileViewModel);
    }


    private void StoreToken(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        Response.Cookies.Append("AccessToken", token, cookieOptions);
    }


    [HttpPost]
    [Authorize]
    [Route("/account/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        Response.Cookies.Delete("AccessToken");
        return RedirectToAction("Index", "Home");
    }
}
