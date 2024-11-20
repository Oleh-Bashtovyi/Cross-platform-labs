using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

using Lab6.ViewModels;

namespace Lab6.Services;

public class Auth0UserService
{
    private readonly string _domain;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _audience;
    private readonly IConfiguration _configuration;

    public Auth0UserService(IConfiguration configuration)
    {
        _configuration = configuration;
        _domain = _configuration["Auth0:Domain"] ?? throw new ArgumentNullException(nameof(configuration));
        _clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException(nameof(configuration));
        _clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException(nameof(configuration));
        _audience = _configuration["Auth0:Audience"] ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task CreateUserAsync(UserRegisterViewModel model)
    {
        var managmentToken = await GetManagmentApiTokenAsync();

        // Create user using managment api token
        var managementClient = new ManagementApiClient(managmentToken, new Uri($"https://{_domain}/api/v2"));
        var userCreateRequest = new UserCreateRequest()
        {
            Email = model.Email,
            EmailVerified = false,
            Password = model.Password,
            Connection = "Username-Password-Authentication",
            UserMetadata = new
            {
                model.FullName,
                model.PhoneNumber,
                model.Username,
            }
        };
        var user = await managementClient.Users.CreateAsync(userCreateRequest);
    }


    public async Task<string> AuthenticateUserAsync(UserLoginViewModel model)
    {
        // Get user access token using email and password
        var authClient = new AuthenticationApiClient(new Uri($"https://{_domain}"));
        var authResponse = await authClient.GetTokenAsync(new ResourceOwnerTokenRequest
        {
            Audience = _audience,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Realm = "Username-Password-Authentication",
            Username = model.Email,
            Password = model.Password,
            Scope = "openid profile email"
        });

        return authResponse.AccessToken;
    }


    public async Task<UserProfileViewModel> GetUserInfo(string token)
    {
        // Get user info using his\her access token
        var authClient = new AuthenticationApiClient(new Uri($"https://{_domain}"));
        var managementClient = new ManagementApiClient(token, new Uri($"https://{_domain}/api/v2"));
        var userInfo = await authClient.GetUserInfoAsync(token);
        var user = await managementClient.Users.GetAsync(userInfo.UserId);

        var alternativeValue = "N/A";

        return new UserProfileViewModel
        {
            Email = user.Email,
            FullName = user.UserMetadata?["FullName"]?.ToString() ?? alternativeValue,
            PhoneNumber = user.UserMetadata?["PhoneNumber"]?.ToString() ?? alternativeValue,
            Username = user.UserMetadata?["Username"]?.ToString() ?? alternativeValue,
            ProfileImage = user.Picture.ToString(),
        };
    }


    public async Task<string> GetManagmentApiTokenAsync()
    {
        var authClient = new AuthenticationApiClient(new Uri($"https://{_domain}"));
        var tokenResponse = await authClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            Audience = _audience,
            ClientId = _clientId,
            ClientSecret = _clientSecret
        });
        return tokenResponse.AccessToken;
    }

}