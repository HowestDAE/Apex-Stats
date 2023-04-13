using ApexStats.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ApexStats.Repositories
{
    internal class MapRotationRepository
    {
        public async Task<MapRotation> GetMapRotationAsync()
        {
            string endpoint = $"https://api.mozambiquehe.re/maprotation";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "559dbc92301ff5c3a23bfc2e3548c5d4");
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JObject>(json).SelectToken("current").ToObject<MapRotation>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
