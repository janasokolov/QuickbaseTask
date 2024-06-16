using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public class GithubClient : IGithubClient
    {
        private readonly HttpClient _httpClient;
        private string _token;
        private readonly string _apiVersion = "2022-11-28";

        public GithubClient(string token)
        {
            _token = token;

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            if (!string.IsNullOrEmpty(_apiVersion))
            {
                _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", _apiVersion);
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com/")
            };

        }

        public async Task<GithubUser> GetUser(string username)
        {
            // Construct the URL
            var url = $"users/{username}";

            // GET request
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content
                var content = await response.Content.ReadAsStringAsync();
                var user = System.Text.Json.JsonSerializer.Deserialize<GithubUser>(content);

                return user;
            }
            else
            {
                throw new Exception($"Failed to retrieve user: {response.ReasonPhrase}");
            }
        }

    }
}
