using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApexStats.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ApexStats.Repositories
{
    internal class APIPlayerStatsRepository : IPlayerStatsRepository
    {
        public async Task<PlayerStatistics> GetPlayerStatsAsync(string username, string platform)
        {
            string endpoint = $"https://public-api.tracker.gg/v2/apex/standard/profile/{platform}/{username}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("TRN-Api-Key", "4e7009c6-da79-4d19-a3a0-750e8bcd6141");
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JObject>(json).SelectToken("data").ToObject<PlayerStatistics>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
