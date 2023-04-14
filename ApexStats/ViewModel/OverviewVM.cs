using ApexStats.Model;
using ApexStats.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using ApexStats.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ApexStats.ViewModel
{
    enum ShopSortCriteria
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
        TokenAsc,
        TokenDesc
    }

    internal class OverviewVM : ObservableObject
    {
        // MAP ROTATION
        public APIMapRotationRepository RotationRepository { get; set; } = new APIMapRotationRepository();

        private MapRotation _currentMapRotation;
        public MapRotation CurrentMapRotation
        {
            get => _currentMapRotation;
            set => SetProperty(ref _currentMapRotation, value);
        }

        // SHOP
        public IShopRepository ShopRepository { get; set; } = new LocalShopRepository();
        
        private List<ShopItem> _shopItems;
        public List<ShopItem> ShopItems
        {
            get => _shopItems;
            set => SetProperty(ref _shopItems, value);
        }

        private ShopSortCriteria _currentSortCriteria;
        public ShopSortCriteria CurrentSortCriteria
        { 
            get => _currentSortCriteria;
            set
            {
                SetProperty(ref _currentSortCriteria, value);
                SortShop();
            }
        }

        // COMMANDS
        public RelayCommand RefreshMapRotationCommand { get; set; }
        public RelayCommand<RepositoryType> RefreshShopCommand { get; set; }
        public RelayCommand SortShopCommand { get; set; }

        public OverviewVM()
        {
            // REFRESH SHOP
            RefreshShopCommand = new RelayCommand<RepositoryType>(async(param) => await RefreshShop(param));
            RefreshShopCommand.Execute(RepositoryType.Local);

            // SORT SHOP
            SortShopCommand = new RelayCommand(SortShop);
            SortShopCommand.Execute(null);

            // REFRESH MAP ROTATION
            RefreshMapRotationCommand = new RelayCommand(async() => await RefreshMapRotation());
            RefreshMapRotationCommand.Execute(null);

            // Timer for map countdown
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async Task RefreshShop(RepositoryType repoType)
        {
            switch (repoType)
            {
                case RepositoryType.Local:
                    ShopRepository = new LocalShopRepository();
                    break;
                case RepositoryType.API:
                    ShopRepository = new APIShopRepository();
                    break;
                default:
                    ShopRepository = new LocalShopRepository();
                    break;
            }
            ShopItems = await ShopRepository.GetShopItemsAsync();     
        }

        private async Task RefreshMapRotation()
        {
            if (CurrentMapRotation?.RemainingTime > 0) return;
            CurrentMapRotation = await RotationRepository.GetMapRotationAsync();
        }

        private void SortShop()
        {
            switch (CurrentSortCriteria)
            {
                case ShopSortCriteria.PriceAsc:
                    ShopItems = ShopItems.OrderBy(item => item.Pricing.First().Quantity).ToList();
                    break;
                case ShopSortCriteria.PriceDesc:
                    ShopItems = ShopItems.OrderByDescending(item => item.Pricing.First().Quantity).ToList();
                    break;
                case ShopSortCriteria.TokenAsc:
                    ShopItems = ShopItems.OrderBy(item => item.Pricing.First().Ref)
                                .ThenBy(item => item.Pricing.First().Quantity).ToList();
                    break;
                case ShopSortCriteria.TokenDesc:
                    ShopItems = ShopItems.OrderBy(item => item.Pricing.First().Ref)
                                .ThenByDescending(item => item.Pricing.First().Quantity).ToList();
                    break;
                case ShopSortCriteria.NameAsc:
                    ShopItems = ShopItems.OrderBy(item => item.Title).ToList();
                    break;
                case ShopSortCriteria.NameDesc:
                    ShopItems = ShopItems.OrderByDescending(item => item.Title).ToList();
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CurrentMapRotation == null || CurrentMapRotation?.RemainingTime <= 0)
            {
                RefreshMapRotationCommand.Execute(null);
            }

            CurrentMapRotation.RemainingTime--;
            OnPropertyChanged(nameof(CurrentMapRotation));
        }
    }
}
