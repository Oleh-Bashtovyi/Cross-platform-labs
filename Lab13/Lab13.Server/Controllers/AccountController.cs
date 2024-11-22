using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Lab13.Server.Models;
using Lab13.Server.Services;

namespace Lab13.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly Auth0UserService _auth0UserService;

    public AccountController(Auth0UserService auth0UserService)
    {
        _auth0UserService = auth0UserService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    k => k.Key,
                    v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new { Errors = errors });
        }

        try
        {
            await _auth0UserService.CreateUserAsync(model);
            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { errors = new { global = ex.Message } });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    k => k.Key,
                    v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new { Errors = errors });
        }

        try
        {
            UserProfileViewModel userProfile = await _auth0UserService.GetUser(model);
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, userProfile.Email),
                    new Claim(ClaimTypes.Name, userProfile.FullName),
                    new Claim(ClaimTypes.Email, userProfile.Email),
                    new Claim("ProfileImage", userProfile.ProfileImage),
                    new Claim(ClaimTypes.MobilePhone, userProfile.PhoneNumber),
                    new Claim("Username", userProfile.Username)
            ];

            var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
            var claimsPrincipal = new ClaimsPrincipal (claimsIdentity);

            await HttpContext.SignInAsync("AuthScheme", claimsPrincipal);


            return Ok(new { Message = "Login successful.", UserProfile = userProfile });
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpGet("profile")]
    [Authorize]
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

        return Ok(profileViewModel);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok(new { Message = "Logout successful." });
    }
}