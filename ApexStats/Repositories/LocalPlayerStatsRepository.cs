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

namespace ApexStats.Repositories
{
    internal class LocalPlayerStatsRepository : IPlayerStatsRepository
    {
        private PlayerStatistics _playerStatistics;
        public PlayerStatistics PlayerStatistics => _playerStatistics;

        public async Task<PlayerStatistics> GetPlayerStatsAsync(string username, string platform)
        {
            if (_playerStatistics != null) return _playerStatistics;

            // Get the assembly
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ApexStats.Resources.Data.playerstats_local.json";

            // Read the JSON file
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    return _playerStatistics = JsonConvert.DeserializeObject<JObject>(json).SelectToken("data").ToObject<PlayerStatistics>();
                }
            }
        }
    }
}
