using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace HubrisEditor.Xaml.Windows
{
    /// <summary>
    /// Interaction logic for ColorSwatch.xaml
    /// </summary>
    public partial class ColorSwatch : Window
    {
        public ColorSwatch()
        {
            InitializeComponent();
            InitializeColors();
        }

        private void InitializeColors()
        {
            if (SystemColors.Count == 0)
            {
                Type colors = typeof(Colors);
                var info = colors.GetProperties();
                foreach (var property in info)
                {
                    SystemColors.Add(new KeyValuePair<string, SolidColorBrush>(property.Name, new SolidColorBrush((Color)ColorConverter.ConvertFromString(property.Name))));
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedItem != null)
            {
                KeyValuePair<string, SolidColorBrush> pair = (KeyValuePair<string, SolidColorBrush>)listBox.SelectedItem;
                Color color = pair.Value.Color;
                HSVColorSwatch.Red = color.R;
                HSVColorSwatch.Green = color.G;
                HSVColorSwatch.Blue = color.B;
                HSVColorSwatch.Alpha = (byte)255;
            }
        }

        public ObservableCollection<KeyValuePair<string, SolidColorBrush>> SystemColors
        {
            get { return (ObservableCollection<KeyValuePair<string, SolidColorBrush>>)GetValue(SystemColorsProperty); }
            set { SetValue(SystemColorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SystemColors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SystemColorsProperty = DependencyProperty.Register("SystemColors", typeof(ObservableCollection<KeyValuePair<string, SolidColorBrush>>), typeof(ColorSwatch), new PropertyMetadata(new ObservableCollection<KeyValuePair<string, SolidColorBrush>>()));
    }
}
