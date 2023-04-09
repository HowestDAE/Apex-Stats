using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ApexStats.View;

namespace ApexStats.ViewModel
{
    internal class MainWindowVM
    {
        public Page CurrentPage { get; set; } = new PlayerStatsPage();
    }
}
