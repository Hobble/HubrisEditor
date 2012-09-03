using HubrisEditor.GameData;
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

namespace HubrisEditor.Xaml.UserControls
{
    /// <summary>
    /// Interaction logic for TileSlotGrid.xaml
    /// </summary>
    public partial class TileSlotGrid : UserControl
    {
        public TileSlotGrid()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RatioHeight = 200.0 * (DataContext as Scenario).CanvasSpaceHeight / (DataContext as Scenario).CanvasSpaceWidth;
        }

        private void Item_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TileSlot slot = (sender as ListBoxItem).DataContext as TileSlot;
            if (e.Key == Key.P)
            {
                slot.SetTileToCurrentlySelected();
            }
            else if (e.Key == Key.D0)
            {
                slot.TileElevation = 0;
            }
            else if (e.Key == Key.D1)
            {
                slot.TileElevation = 1;
            }
            else if (e.Key == Key.D2)
            {
                slot.TileElevation = 2;
            }
            else if (e.Key == Key.D3)
            {
                slot.TileElevation = 3;
            }
            else if (e.Key == Key.D4)
            {
                slot.TileElevation = 4;
            }
            else if (e.Key == Key.D5)
            {
                slot.TileElevation = 5;
            }
            else if (e.Key == Key.D6)
            {
                slot.TileElevation = 6;
            }
            else if (e.Key == Key.D7)
            {
                slot.TileElevation = 7;
            }
            else if (e.Key == Key.D8)
            {
                slot.TileElevation = 8;
            }
            else if (e.Key == Key.Delete)
            {
                slot.TileContentEnum = 0;
            }
            else if (e.Key == Key.I)
            {
                slot.TileContentEnum = 1;
            }
            else if (e.Key == Key.R)
            {
                slot.TileContentEnum = 2;
            }
            else if (e.Key == Key.E)
            {
                slot.TileContentEnum = 3;
            }
            else if (e.Key == Key.B)
            {
                slot.TileContentEnum = 4;
            }
            else if (e.Key == Key.S)
            {
                slot.TileContentEnum = 5;
            }
            else if (e.Key == Key.F)
            {
                slot.TileContentEnum = 6;
            }
            else if (e.Key == Key.D)
            {
                slot.TileContentEnum = 7;
            }
            else if (e.Key == Key.N)
            {
                slot.TileContentEnum = 8;
            }
            else if (e.Key == Key.U)
            {
                slot.TileContentEnum = 9;
            }
            else if (e.Key == Key.H)
            {
                slot.TileContentEnum = 10;
                e.Handled = true;
            }
            else if (e.Key == Key.V)
            {
                slot.TileContentEnum = 11;
            }
        }

        private void TilesListBox_Loaded(object sender, RoutedEventArgs e)
        {
            TileTypeItemsPresenter = sender as ItemsPresenter;
        }

        public ItemsPresenter TileTypeItemsPresenter
        {
            get { return (ItemsPresenter)GetValue(TileTypeItemsPresenterProperty); }
            set { SetValue(TileTypeItemsPresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TileTypeItemsPresenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TileTypeItemsPresenterProperty = DependencyProperty.Register("TileTypeItemsPresenter", typeof(ItemsPresenter), typeof(TileSlotGrid), new PropertyMetadata(null));

        public double RatioHeight
        {
            get { return (double)GetValue(RatioHeightProperty); }
            set { SetValue(RatioHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatioHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatioHeightProperty = DependencyProperty.Register("RatioHeight", typeof(double), typeof(TileSlotGrid), new PropertyMetadata(200.0));
    }
}
