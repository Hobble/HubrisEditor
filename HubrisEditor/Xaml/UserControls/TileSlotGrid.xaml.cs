using HubrisEditor.GameData;
using HubrisEditor.Xaml.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            m_selection = new List<TileSlot>();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext as Scenario != null)
            {
                RatioHeight = 200.0 * (DataContext as Scenario).CanvasSpaceHeight / (DataContext as Scenario).CanvasSpaceWidth;
                (DataContext as Scenario).TilesGenerated += TileSlotGrid_TilesGenerated;
            }
        }

        private void TileSlotGrid_TilesGenerated(object sender, EventArgs e)
        {
            RatioHeight = 200.0 * (DataContext as Scenario).CanvasSpaceHeight / (DataContext as Scenario).CanvasSpaceWidth;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer senderCast = sender as ScrollViewer;
            PanZoomHost contentCast = senderCast.Content as PanZoomHost;
            ListBox child = contentCast.Content as ListBox;
            e.Handled = true;
            if (e.Delta > 0)
            {
                Point curContentMousePoint = e.GetPosition(child);
                ZoomIn(curContentMousePoint, contentCast);
            }
            else if (e.Delta < 0)
            {
                Point curContentMousePoint = e.GetPosition(child);
                ZoomOut(curContentMousePoint, contentCast);
            }
        }

        private void ZoomOut(Point contentZoomCenter, PanZoomHost host)
        {
            host.ZoomAboutPoint(host.ContentScale - 0.1, contentZoomCenter);
        }

        private void ZoomIn(Point contentZoomCenter, PanZoomHost host)
        {
            host.ZoomAboutPoint(host.ContentScale + 0.1, contentZoomCenter);
        }

        private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                IsPanningSystemActive = true;
            }
            else
            {
                foreach (var slot in m_selection)
                {
                    if (e.Key == Key.P)
                    {
                        slot.SetTileToCurrentlySelected();
                    }
                    if (e.Key == Key.U)
                    {
                        slot.SetPlacementToCurrentlySelected();
                    }
                    if (e.Key == Key.C)
                    {
                        slot.SetContentToCurrentlySelected();
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
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPanningSystemActive)
            {
                m_originalMousePosition = e.GetPosition(sender as IInputElement);
                Mouse.Capture(sender as IInputElement);
                IsPanning = true;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPanning)
            {
                Point currentMousePosition = e.GetPosition(sender as IInputElement);
                double deltax = currentMousePosition.X - m_originalMousePosition.X;
                double deltay = currentMousePosition.Y - m_originalMousePosition.Y;
                m_originalMousePosition = currentMousePosition;
                PanZoomHost host = (((sender as Canvas).Parent as Grid).Children[0] as ScrollViewer).Content as PanZoomHost;
                OffsetX = Math.Min(Math.Max(OffsetX - deltax, 0.0), host.CoerceWidthMax);
                OffsetY = Math.Min(Math.Max(OffsetY - deltay, 0.0), host.CoerceHeightMax);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsPanning = false;
            Mouse.Capture(null);
        }

        private void ScrollViewer_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                IsPanningSystemActive = false;
                IsPanning = false;
                Mouse.Capture(null);
            }
        }

        private void TilesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!m_forceSelectionChanges)
            {
                m_forceSelectionChanges = true;

                m_selection.Clear();
                ElevationListBox.SelectedItems.Clear();
                UnitPlacementListBox.SelectedItems.Clear();
                TileContentListBox.SelectedItems.Clear();

                foreach (TileSlot item in TilesListBox.SelectedItems)
                {
                    m_selection.Add(item);
                    ElevationListBox.SelectedItems.Add(item);
                    UnitPlacementListBox.SelectedItems.Add(item);
                    TileContentListBox.SelectedItems.Add(item);
                }

                m_forceSelectionChanges = false;
            }
        }

        private void ElevationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!m_forceSelectionChanges)
            {
                m_forceSelectionChanges = true;

                m_selection.Clear();
                TilesListBox.SelectedItems.Clear();
                UnitPlacementListBox.SelectedItems.Clear();
                TileContentListBox.SelectedItems.Clear();

                foreach (TileSlot item in ElevationListBox.SelectedItems)
                {
                    m_selection.Add(item);
                    TilesListBox.SelectedItems.Add(item);
                    UnitPlacementListBox.SelectedItems.Add(item);
                    TileContentListBox.SelectedItems.Add(item);
                }

                m_forceSelectionChanges = false;
            }
        }

        private void UnitPlacementListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!m_forceSelectionChanges)
            {
                m_forceSelectionChanges = true;

                m_selection.Clear();
                TilesListBox.SelectedItems.Clear();
                ElevationListBox.SelectedItems.Clear();
                TileContentListBox.SelectedItems.Clear();

                foreach (TileSlot item in UnitPlacementListBox.SelectedItems)
                {
                    m_selection.Add(item);
                    TilesListBox.SelectedItems.Add(item);
                    ElevationListBox.SelectedItems.Add(item);
                    TileContentListBox.SelectedItems.Add(item);
                }

                m_forceSelectionChanges = false;
            }
        }

        private void TileContentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!m_forceSelectionChanges)
            {
                m_forceSelectionChanges = true;

                m_selection.Clear();
                TilesListBox.SelectedItems.Clear();
                ElevationListBox.SelectedItems.Clear();
                UnitPlacementListBox.SelectedItems.Clear();

                foreach (TileSlot item in TileContentListBox.SelectedItems)
                {
                    m_selection.Add(item);
                    TilesListBox.SelectedItems.Add(item);
                    ElevationListBox.SelectedItems.Add(item);
                    UnitPlacementListBox.SelectedItems.Add(item);
                }

                m_forceSelectionChanges = false;
            }
        }

        public ReadOnlyCollection<TileSlot> SelectedTiles
        {
            get
            {
                return new ReadOnlyCollection<TileSlot>(m_selection);
            }
        }

        public double RatioHeight
        {
            get { return (double)GetValue(RatioHeightProperty); }
            set { SetValue(RatioHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatioHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatioHeightProperty = DependencyProperty.Register("RatioHeight", typeof(double), typeof(TileSlotGrid), new PropertyMetadata(200.0));

        public bool IsPanningSystemActive
        {
            get { return (bool)GetValue(IsPanningSystemActiveProperty); }
            set { SetValue(IsPanningSystemActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPanningSystemActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPanningSystemActiveProperty = DependencyProperty.Register("IsPanningSystemActive", typeof(bool), typeof(TileSlotGrid), new PropertyMetadata(false));

        public bool IsPanning
        {
            get { return (bool)GetValue(IsPanningProperty); }
            set { SetValue(IsPanningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPanning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPanningProperty = DependencyProperty.Register("IsPanning", typeof(bool), typeof(TileSlotGrid), new PropertyMetadata(false));

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register("OffsetX", typeof(double), typeof(TileSlotGrid), new PropertyMetadata(0.0));

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register("OffsetY", typeof(double), typeof(TileSlotGrid), new PropertyMetadata(0.0));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(TileSlotGrid), new PropertyMetadata(1.0));

        private List<TileSlot> m_selection;
        private bool m_forceSelectionChanges;
        private Point m_originalMousePosition;
    }
}
