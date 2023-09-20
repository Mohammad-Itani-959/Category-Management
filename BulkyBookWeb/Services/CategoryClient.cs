using BulkyBookWeb.Model;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BulkyBookWeb.Services
{
    public class CategoryClient
    {
       /* private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CategoryClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Configure the base address
            _httpClient.BaseAddress = new Uri("https://localhost:7025/api/Category");

            // Configure the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["RecipeEngine:BearerToken"]);

            // Bypass SSL certificate validation (for testing purposes only)
            var bypassSslValidation = bool.Parse(_configuration["BypassSslValidation"] ?? "false");
            if (bypassSslValidation)
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
                _httpClient = new HttpClient(handler);
            }
        }*/

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CategoryClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

           
            //add 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "CategoryAPI");
            //set the url that i want to use 

            _httpClient.BaseAddress = new Uri("http://localhost:7025/api/");



            _httpClient.Timeout = TimeSpan.FromSeconds(30);

        }
        // Get Category for Edit
        public async Task<Category>  GetCategoryForEdit(int id)
        { 
               var response = await _httpClient.GetStringAsync($"Category/GetById/{id}");
               
               Category category = JsonConvert.DeserializeObject<Category>(response);

               return category;
        }
        //Create is done
        public async Task Create(Category category)
        {
            var json = JsonConvert.SerializeObject(category);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Category/Create", data);
            response.EnsureSuccessStatusCode();
            var responsebody = await response.Content.ReadAsStringAsync();
        }
        //done
        public async Task UpdateCategory(Category category)
        {
            // I will convert the object category to json format 
            var json = JsonConvert.SerializeObject(category);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Category/update/{category.Id}", data);
            response.EnsureSuccessStatusCode();
            var responsebody = await response.Content.ReadAsStringAsync();



        }

        public async Task DeleteCategory(Category category)
        {
            var json = JsonConvert.SerializeObject(category);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.DeleteAsync($"Category/Delete/{category.Id}");
            response.EnsureSuccessStatusCode();
            var responsebody = await response.Content.ReadAsStringAsync();

        }
        
    }
}
