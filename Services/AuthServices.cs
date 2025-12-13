using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mini_Project_Kredit.Models;

namespace Mini_Project_Kredit.Services;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<AuthResponse?> Login(string username, string password)
    {
        var url = "http://api.inixindojogja.com/index.php/svc/get_token";

        var creds = Encoding.ASCII.GetBytes($"{username}:{password}");
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(creds));

        var response = await _http.PostAsync(url, null);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResponse>(json);
    }

}
