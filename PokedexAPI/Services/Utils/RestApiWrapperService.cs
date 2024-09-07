using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Formatting;

namespace PokedexAPI.Services.Utils
{
    public class RestApiWrapperService
    {
        /// <summary>
        /// For getting the resources from a web api
        /// </summary>
        /// <param name="url">API Url</param>
        /// <returns>A Task with result object of type T</returns>
        public static async Task<T?> Get<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;

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
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public static async Task<T> PostRequest<T>(string apiUrl, T postObject)
        {
            using (var client = new HttpClient())
            {
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

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public static async Task PutRequest<T>(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}

