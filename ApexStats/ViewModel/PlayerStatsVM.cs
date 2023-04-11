using ApexStats.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApexStats.Repositories;

namespace ApexStats.ViewModel
{
    internal class PlayerStatsVM : ObservableObject
    {
        public IPlayerStatsRepository PlayerStatsRepository { get; set; } = new LocalPlayerStatsRepository();

        private PlayerStatistics _playerStatistics;
        public PlayerStatistics PlayerStatistics
        {
            get => _playerStatistics;
            set => SetProperty(ref _playerStatistics, value);
        }

        private Segment _playerSegment;
        public Segment PlayerSegment
        {
            get => _playerSegment;
            set => SetProperty(ref _playerSegment, value);
        }

        private List<Segment> _legendSegments = new List<Segment>();
        public List<Segment> LegendSegments
        {
            get => _legendSegments;
            set => SetProperty(ref _legendSegments, value);
        }

        public RelayCommand GetPlayerStatsCommand { get; set; }

        public PlayerStatsVM()
        { 
            GetPlayerStatsCommand = new RelayCommand(async() => await GetPlayerStatsAsync());
            GetPlayerStatsCommand.Execute(this);
        }

        private async Task GetPlayerStatsAsync()
        {
            _playerStatistics = await PlayerStatsRepository.GetPlayerStatsAsync("MemesKeepMeLivin", "origin");
            GetAcountData();
            GetLegendData();
        }
        
        /// <summary>
        /// Gets the player acount data (acount level, total kills, wins...)
        /// </summary>
        private void GetAcountData()
        {
            _playerSegment = _playerStatistics.Segments[0];
        }

        /// <summary>
        /// Gets legend data of all legends with three or more statistics
        /// </summary>
        private void GetLegendData()
        {
            foreach (var segment in _playerStatistics.Segments)
            {
                // Segment is a legend with 3 or more statistics
                if (segment.Type == "legend" && segment.Statistics.Count >= 1)
                    _legendSegments.Add(segment);
            }
        }
    }
}
