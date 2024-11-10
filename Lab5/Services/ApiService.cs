using Lab5.DTO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab5.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly Auth0UserService _auth0UserService;
    private readonly JsonSerializerOptions _serializationOptions;

    public ApiService(HttpClient httpClient, Auth0UserService auth0UserService)
    {
        _httpClient = httpClient;
        _auth0UserService = auth0UserService;
        _serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
        };
    }


    private async Task SetAuthorizationHeaderAsync()
    {
        var accessToken = await _auth0UserService.GetAccessTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }



    public async Task<List<Diver>> GetDiversAsync()
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync("api/v1/Divers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDivers] - Received JSON: {json}");

        var divers = JsonSerializer.Deserialize<List<Diver>>(json, _serializationOptions);

        return divers ?? new List<Diver>(0);
    }

    public async Task<Diver?> GetDiverAsync(Guid id)
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync($"api/v1/Divers/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiver] - Received JSON: {json}");

        var diver = JsonSerializer.Deserialize<Diver>(json, _serializationOptions);

        return diver;
    }


    public async Task<List<DiveSite>> GetDiveSitesAsync()
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync("api/v1/DiveSites");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiveSites] - Received JSON: {json}");

        var diveSites = JsonSerializer.Deserialize<List<DiveSite>>(json, _serializationOptions);

        return diveSites ?? new List<DiveSite>(0);
    }

    public async Task<DiveSite?> GetDiveSiteAsync(Guid id)
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync($"api/v1/DiveSites/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiveSite] - Received JSON: {json}");

        var diveSite = JsonSerializer.Deserialize<DiveSite>(json, _serializationOptions);

        return diveSite;
    }






    public async Task<List<Dive>> GetDivesAsync(string apiVersion = "v1")
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync($"api/{apiVersion}/Dives");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDives] - Received JSON: {json}");

        var dives = JsonSerializer.Deserialize<List<Dive>>(json, _serializationOptions);

        return dives ?? new List<Dive>(0);
    }

    public async Task<Dive?> GetDiveAsync(Guid id, string apiVersion = "v1")
    {
        await SetAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync($"api/{apiVersion}/Dives/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDives] - Received JSON: {json}");

        var dive = JsonSerializer.Deserialize<Dive>(json, _serializationOptions);

        return dive;
    }







    //public async Task<IEnumerable<DiveOrganisation>> GetProtectedDataAsync()
    //{
    //    var accessToken = await _auth0UserService.GetAccessTokenAsync();
    //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    //    var response = await _httpClient.GetAsync("https://localhost:7142/api/v1/DiveOrganisations");
    //    response.EnsureSuccessStatusCode();

    //    //return await response.Content.ReadFromJsonAsync<IEnumerable<DiveOrganisation>>();


    //    var json = await response.Content.ReadAsStringAsync();
    //    // Log the JSON response for debugging
    //    Console.WriteLine($"Received JSON: {json}");

    //    var options = new JsonSerializerOptions
    //    {
    //        PropertyNameCaseInsensitive = true
    //    };

    //    // Deserialize the JSON into the AccountList object
    //    DiveOrganisationList accountLists = JsonSerializer.Deserialize<DiveOrganisationList>(json, options);

    //    // Return the list of accounts from the AccountList object
    //    return accountLists.Values; // Assuming AccountList has a property called Accounts
    //}
}


//public class DiveOrganisation
//{
//    [JsonPropertyName("OrganisationCode")]
//    public string OrganisationCode { get; set; }

//    [JsonPropertyName("CountryOfOrigin")]
//    public string CountryOfOrigin { get; set; }

//    [JsonPropertyName("OrganisationDetails")]
//    public string OrganisationDetails { get; set; }
//}




//public class DiveOrganisationList
//{
//    [JsonPropertyName("$id")]
//    public string Id { get; set; }

//    [JsonPropertyName("$values")]
//    public List<DiveOrganisation> Values { get; set; }
//}