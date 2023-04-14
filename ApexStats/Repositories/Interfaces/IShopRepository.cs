using ApexStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Repositories.Interfaces
{
    internal interface IShopRepository
    {
        Task<List<ShopItem>> GetShopItemsAsync();
    }
}
