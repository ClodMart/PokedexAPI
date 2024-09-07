using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokedexAPI.Classes;
using System.Net.Http.Headers;
using System.Text;

namespace PokedexAPI.Services.Utils
{
    //Wrapper of Http requests
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string BaseUrl;

        public ApiService(string BaseUri)
        {
            BaseUrl = BaseUri;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
           // _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Method to perform a GET request
        public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? Parameters = null)
        {
            try
            {   

                if (Parameters != null)
                { // Add parameters to request
                    var Params = new FormUrlEncodedContent(Parameters);
                    var queryString = await Params.ReadAsStringAsync();
                    endpoint = endpoint+"?" + queryString;
                }
               

                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (HttpRequestException e)
            {
                // Handle exception (log it, rethrow, etc.)
                throw new Exception($"Error fetching data from the API: {e.Message}", e);
            }
        }

        // Method to perform a POST request
        public async Task<T> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (HttpRequestException e)
            {
                // Handle exception (log it, rethrow, etc.)
                throw new Exception($"Error posting data to the API: {e.Message}", e);
            }
        }
    }
}
