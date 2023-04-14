using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ApexStats.View
{
    /// <summary>
    /// Interaction logic for OverviewPage.xaml
    /// </summary>
    public partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView view = 
                CollectionViewSource.GetDefaultView(lvShop.ItemsSource) as CollectionView;

            view.SortDescriptions.Clear();

            var selectedValue = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();

            switch (selectedValue)
            {
                case "Price Asc.":
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Quantity", ListSortDirection.Ascending));
                    break;
                case "Price Desc.":
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Quantity", ListSortDirection.Descending));
                    break;
                case "Token Asc.":
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Ref", ListSortDirection.Ascending));
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Quantity", ListSortDirection.Ascending));
                    break;
                case "Token Desc.":
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Ref", ListSortDirection.Ascending));
                    view.SortDescriptions.Add(new SortDescription("Pricing[0].Quantity", ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
                
        }
    }
}
