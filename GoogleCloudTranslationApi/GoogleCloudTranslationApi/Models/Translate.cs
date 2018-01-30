using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleCloudTranslationApi.Models
{
    class MTranslate
    {
        static readonly string APIKEY = "77440b25011241939bd1be1813d1e79a";
        static readonly string from = "tr";
        static readonly string to = "en";
        public static string Main(string text)
        {
            string fin = string.Empty;

            Task.Run(async () =>
            {
                var accessToken = await GetAuthenticationToken(APIKEY);
                var output = await Translate(text, from, to, accessToken);
                fin = output;

            }).Wait();

            return fin;

        }

        static async Task<string> Translate(string textToTranslate, string from, string to, string accessToken)
        {
            string url = "http://api.microsofttranslator.com/v2/Http.svc/Translate";
            string query = $"?text={System.Net.WebUtility.UrlEncode(textToTranslate)}&from={from}&to={to}&contentType=text/plain";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(url + query);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return "ERROR: " + result;

                var translatedText = XElement.Parse(result).Value;
                return translatedText;
            }
        }

        static async Task<string> GetAuthenticationToken(string key)
        {
            string endpoint = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                var response = await client.PostAsync(endpoint, null);
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
        }
    }
}