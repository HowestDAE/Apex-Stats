using ApexStats.Model;
using ApexStats.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ApexStats.ViewModel
{
    enum RepositoryType
    {
        Local,
        API
    }

    internal class PlayerStatsVM : ObservableObject
    {
        public IPlayerStatsRepository PlayerStatsRepository { get; set; }

        // USER CREDENTIALS
        public string Username { get; set; }
        public string Platform { get; set; }

        public List<string> Platforms { get; set; } = new List<string> { "origin", "psn", "xbl" };

        private PlayerStatistics _playerStatistics;
        public PlayerStatistics PlayerStatistics
        {
            get => _playerStatistics;
            set => SetProperty(ref _playerStatistics, value);
        }

        private Segment _accountSegment;
        public Segment AccountSegment
        {
            get => _accountSegment;
            set => SetProperty(ref _accountSegment, value);
        }

        private List<Segment> _legendSegments = new List<Segment>();
        public List<Segment> LegendSegments
        {
            get => _legendSegments;
            set => SetProperty(ref _legendSegments, value);
        }

        public RelayCommand<RepositoryType> GetPlayerStatsCommand { get; set; }

        public PlayerStatsVM()
        {
            GetPlayerStatsCommand = new RelayCommand<RepositoryType>(async(param) => await GetPlayerStatsAsync(param));
        }

        private async Task GetPlayerStatsAsync(RepositoryType repoType)
        {
            switch (repoType)
            {
                case RepositoryType.Local:
                    PlayerStatsRepository = new LocalPlayerStatsRepository();
                    break;
                case RepositoryType.API:
                    PlayerStatsRepository = new APIPlayerStatsRepository();
                    break;
                default:
                    PlayerStatsRepository = new LocalPlayerStatsRepository();
                    break;
            }
            PlayerStatistics = await PlayerStatsRepository.GetPlayerStatsAsync(Username, Platform);

            // Something might go wrong when fetching data from the API (Too Many Requests, API unreachable, ...)
            // or user did not enter all credentials, load local data instead.
            if (PlayerStatistics == null)
            {
                PlayerStatsRepository = new LocalPlayerStatsRepository();
                PlayerStatistics = await PlayerStatsRepository.GetPlayerStatsAsync(Username, Platform);
            }

            AccountSegment = PlayerStatistics.Segments[0];

            // Get the Legend data
            var legendSegments = new List<Segment>();
            foreach (var segment in PlayerStatistics.Segments)
            {
                // The API sometimes fetches stats of a legend with a null value
                // Remove these stats from the legend to prevent any issues with binding
                var keysToRemove = new List<string>();

                foreach(var stat in segment.Statistics)
                    if (stat.Value.Percentile == null) keysToRemove.Add(stat.Key);

                foreach (var key in keysToRemove)
                    segment.Statistics.Remove(key);

                // Only get data from legends that have one or more statistics
                if (segment.Type == "legend" && segment.Statistics.Count >= 1)
                    legendSegments.Add(segment);
            }

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                // Update the LegendSegments property with the new List on the UI Thread to prevent a crash
                LegendSegments = legendSegments;
            });
        }
    }
}
