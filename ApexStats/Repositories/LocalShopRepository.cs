using ApexStats.Model;
using ApexStats.Repositories.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Repositories
{
    internal class LocalShopRepository : IShopRepository
    {
        private List<ShopItem> _shopItems;

        public async Task<List<ShopItem>> GetShopItemsAsync()
        {
            if (_shopItems != null) return _shopItems;

            // Get the assembly
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ApexStats.Resources.Data.shop_local.json";

            // Read the JSON file
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    return _shopItems = JsonConvert.DeserializeObject<List<ShopItem>>(json);
                }
            }
        }
    }
}
