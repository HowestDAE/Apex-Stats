using ApexStats.Model;
using ApexStats.Repositories.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Repositories
{
    internal class APIShopRepository : IShopRepository
    {

        public async Task<List<ShopItem>> GetShopItemsAsync()
        {
            string endpoint = $"https://api.mozambiquehe.re/store";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "559dbc92301ff5c3a23bfc2e3548c5d4");
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ShopItem>>(json);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
