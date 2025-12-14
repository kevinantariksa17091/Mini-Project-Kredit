using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using Mini_Project_Kredit.Models;

namespace Mini_Project_Kredit.Services
{
    public class VillageService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        // cache token sederhana
        private static string? _cachedToken;
        private static DateTime _tokenCreatedAt;

        public VillageService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        /// <summary>
        /// Ambil token dengan Basic Auth
        /// </summary>
        private async Task<string> GetTokenAsync()
        {
            // gunakan token lama jika masih fresh (±55 menit)
            if (!string.IsNullOrEmpty(_cachedToken) &&
                DateTime.UtcNow.Subtract(_tokenCreatedAt).TotalMinutes < 55)
            {
                return _cachedToken!;
            }

            var tokenUrl = "http://api.inixindojogja.com/index.php/svc/get_token";
            var username = _config["InixApi:Username"];
            var password = _config["InixApi:Password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Credential INIX API belum dikonfigurasi.");

            var authValue = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}")
            );

            var request = new HttpRequestMessage(HttpMethod.Get, tokenUrl);
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", authValue);

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var tokenResponse =
                await response.Content.ReadFromJsonAsync<InixTokenResponse>();

            if (tokenResponse == null || tokenResponse.status != 1)
                throw new Exception("Gagal mendapatkan token API desa.");

            _cachedToken = tokenResponse.token;
            _tokenCreatedAt = DateTime.UtcNow;

            return _cachedToken!;

        }

        /// <summary>
        /// POST get_desa/{token}
        /// </summary>
        public async Task<List<DesaApiRow>> SearchVillageAsync(string search)
        {
            var token = await GetTokenAsync();
            var url = $"http://api.inixindojogja.com/index.php/svc/get_desa/{token}";

            using var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["search"] = search
            });

            var response = await _http.PostAsync(url, content);

            var raw = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new List<DesaApiRow>();

            var data = System.Text.Json.JsonSerializer.Deserialize<DesaApiResponse>(
                raw,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return data?.rows ?? new List<DesaApiRow>();
        }
    }
}
