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
    public class FreshdeskClient : IFreshdeskClient
    {
        private readonly HttpClient _httpClient;
        private string _token;


        public FreshdeskClient(string token, string subdomain)
        {
            _token = token;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"https://{subdomain}.freshdesk.com/api/v2/")
            };
        }


        public async Task<Contact> GetContact(string freshdeskSubdomain, string email)
        {
            // Construct the URL with query parameters
            var url = $"contacts?email={Uri.EscapeDataString(email)}";

            // GET request
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content
                var content = await response.Content.ReadAsStringAsync();
                var contacts = JsonSerializer.Deserialize<Contact[]>(content);

                // Return the first contact found or null if none are found
                return contacts?.Length > 0 ? contacts[0] : null;
            }
            else
            {
                throw new Exception($"Failed to retrieve user: {response.ReasonPhrase}");
            }
        }

        public async Task<Contact> CreateContact(string subdomain, CreateContactInput createContactInput)
        {
            // Convert the CreateContactInput object to JSON
            var jsonContent = JsonSerializer.Serialize(createContactInput);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Make the POST request to create a new contact
            var response = await _httpClient.PostAsync("contacts", content);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var createdContact = JsonSerializer.Deserialize<Contact>(responseContent);

                return createdContact;
            }
            else
            {
                throw new Exception($"Failed to create contact: {response.ReasonPhrase}");
            }
        }

        public async Task<Contact> UpdateContact(string subdomain, int id, UpdateContactInput updateContactInput)
        {
            // Convert the UpdateContactInput object to JSON
            var jsonContent = JsonSerializer.Serialize(updateContactInput);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Make the PUT request to update the contact
            var response = await _httpClient.PutAsync($"contacts/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var updatedContact = JsonSerializer.Deserialize<Contact>(responseContent);

                return updatedContact;
            }
            else
            {
                throw new Exception($"Failed to update contact: {response.ReasonPhrase}");
            }
        }
    }
}
