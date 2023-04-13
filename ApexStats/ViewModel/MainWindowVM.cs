using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ApexStats.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApexStats.ViewModel
{
    internal class MainWindowVM : ObservableObject
    {
        public Page OverviewPage { get; set; } = new OverviewPage();

        public Page PlayerStatsPage { get; set; } = new PlayerStatsPage();

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        // USER CREDENTIALS
        public string Username { get; set; }
        public string Platform { get; set; }

        public string ButtonText
        {
            get
            {
                if (_currentPage == OverviewPage) return "USE LOCAL DATA";
                else return "BACK TO OVERVIEW";
            }
        }
        public List<string> Platforms { get; set; } = new List<string> { "origin", "psn", "xbl" };

        public RelayCommand SearchPlayerCommand { get; set; }
        public RelayCommand SecondaryButtonCommand { get; set; }

        public MainWindowVM()
        {
            CurrentPage = OverviewPage;
            SearchPlayerCommand = new RelayCommand(SearchPlayer);
            SecondaryButtonCommand = new RelayCommand(SecondaryButtonPressed);
        }

        private void SearchPlayer()
        {
            var playerStatsVM = PlayerStatsPage.DataContext as PlayerStatsVM;
            playerStatsVM.Username = Username;
            playerStatsVM.Platform = Platform;
            playerStatsVM.GetPlayerStatsCommand.Execute(RepositoryType.API);

            if(CurrentPage != PlayerStatsPage)
            {
                CurrentPage = PlayerStatsPage;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private void SecondaryButtonPressed()
        {
            if(CurrentPage == PlayerStatsPage)
            {
                CurrentPage = OverviewPage;
            }
            else
            {
                (PlayerStatsPage.DataContext as PlayerStatsVM).GetPlayerStatsCommand.Execute(RepositoryType.Local);
                CurrentPage = PlayerStatsPage;
            }

            OnPropertyChanged(nameof(ButtonText));
        }
    }
}
