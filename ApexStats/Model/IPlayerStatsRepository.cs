using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Model
{
    internal interface IPlayerStatsRepository
    {
        Task<PlayerStatistics> GetPlayerStatsAsync(string username, string platform);
    }
}
