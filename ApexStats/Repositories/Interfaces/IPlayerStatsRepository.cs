using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexStats.Model;

namespace ApexStats.Repositories
{
    internal interface IPlayerStatsRepository
    {
        Task<PlayerStatistics> GetPlayerStatsAsync(string username, string platform);
    }
}
