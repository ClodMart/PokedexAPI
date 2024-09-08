using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Formatting;
using System.Web;

namespace PokedexAPI.Services.Utils
{
    public class RestApiWrapperService
    {

        /// <summary>
        /// For getting the resources from a web api
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="queryParams">query params to add to the request</param>
        /// <returns>A Task with result object of type T</returns>
        public static async Task<T?> Get<T>(string apiUrl, Dictionary<string, string>? queryParams = null)
        {
            using (var httpClient = new HttpClient())
            {
                if(queryParams != null)
                {
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    foreach (var item in queryParams)
                    {
                        query[item.Key] = item.Value;
                    }
                    apiUrl += "?" + query.ToString();
                }

                var response = httpClient.GetAsync(new Uri(apiUrl)).Result;

                if(response.StatusCode != HttpStatusCode.OK) {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

               T result =  await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                    {
                        throw x.Exception;
                    }

                    T result = JsonConvert.DeserializeObject<T>(x.Result);
                    if (result == null)
                    {
                        throw new Exception("null ref");
                    }
                    return result;
                });
                return result;
            }            
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="queryParams">query params to add to the request</param>
        /// <param name="postObject">The object to be passed as payload</param>
        /// <returns>A Task with request result</returns>
        public static async Task<T> PostRequest<T>(string apiUrl, Dictionary<string, string>? queryParams = null, object? postObject = null)
        {
            using (var client = new HttpClient())
            {
                if (queryParams != null)
                {
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    foreach (var item in queryParams)
                    {
                        query[item.Key] = item.Value;
                    }
                    apiUrl += "?" +query.ToString();
                }
                var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                T result = await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    T result = JsonConvert.DeserializeObject<T>(x.Result);
                    if (result == null)
                    {
                        throw new Exception("null ref");
                    }
                    return result;
                });
                return result;
            }            
        }
    }
}

