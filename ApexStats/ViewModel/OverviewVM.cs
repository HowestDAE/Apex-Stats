using ApexStats.Model;
using ApexStats.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ApexStats.ViewModel
{
    internal class OverviewVM : ObservableObject
    {
        public MapRotationRepository RotationRepository { get; set; } = new MapRotationRepository();

        private MapRotation _currentMapRotation;
        public MapRotation CurrentMapRotation
        {
            get => _currentMapRotation;
            set => SetProperty(ref _currentMapRotation, value);
        }

        public RelayCommand RefreshCommand { get; set; }

        public OverviewVM()
        {
            RefreshCommand = new RelayCommand(async() => await Refresh());
            RefreshCommand.Execute(null);

            // Timer for map countdown
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async Task Refresh()
        {
            if (CurrentMapRotation?.RemainingTime > 0) return;
            CurrentMapRotation = await RotationRepository.GetMapRotationAsync();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CurrentMapRotation == null || CurrentMapRotation?.RemainingTime <= 0)
            {
                RefreshCommand.Execute(null);
                return;
            }

            CurrentMapRotation.RemainingTime--;
            OnPropertyChanged(nameof(CurrentMapRotation));
        }
    }
}
