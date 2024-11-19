using Lab6.DTO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab6.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializationOptions;

    public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;

        var accessToken = httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"] ?? "";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        _serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
        };
    }


    public async Task<List<DiverResponse>> GetDiversAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/Divers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDivers] - Received JSON: {json}");

        var divers = JsonSerializer.Deserialize<List<DiverResponse>>(json, _serializationOptions);

        return divers ?? new List<DiverResponse>(0);
    }

    public async Task<DiverResponse?> GetDiverAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/Divers/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiver] - Received JSON: {json}");

        var diver = JsonSerializer.Deserialize<DiverResponse>(json, _serializationOptions);

        return diver;
    }


    public async Task<List<DiveSiteResponse>> GetDiveSitesAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/DiveSites");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiveSites] - Received JSON: {json}");

        var diveSites = JsonSerializer.Deserialize<List<DiveSiteResponse>>(json, _serializationOptions);

        return diveSites ?? new List<DiveSiteResponse>(0);
    }


    public async Task<DiveSiteResponse?> GetDiveSiteAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/DiveSites/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDiveSite] - Received JSON: {json}");

        var diveSite = JsonSerializer.Deserialize<DiveSiteResponse>(json, _serializationOptions);

        return diveSite;
    }


    public async Task<List<DiveResponse>> GetDivesAsync(DiveRequest request, string apiVersion = "v1")
    {
        var query = $"api/{apiVersion}/dives?" +
                    $"startDate={request.StartDate?.ToString("yyyy-MM-ddTHH:mm:ss")}&" +
                    $"endDate={request.EndDate?.ToString("yyyy-MM-ddTHH:mm:ss")}&" +
                    $"diverId={request.DiverId}&" +
                    $"siteNameStart={request.SiteNameStart}&" +
                    $"siteNameEnd={request.SiteNameEnd}";

        var response = await _httpClient.GetAsync(query);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<DiveResponse>>(json, _serializationOptions) ?? new List<DiveResponse>();
    }


    public async Task<DiveResponse?> GetDiveAsync(Guid id, string apiVersion = "v1")
    {
        var response = await _httpClient.GetAsync($"api/{apiVersion}/Dives/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[GetDive] - Received JSON: {json}");

        var dive = JsonSerializer.Deserialize<DiveResponse>(json, _serializationOptions);

        return dive;
    }
}