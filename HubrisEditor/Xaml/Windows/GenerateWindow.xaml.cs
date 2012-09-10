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
using System.Windows.Shapes;

namespace HubrisEditor.Xaml.Windows
{
    /// <summary>
    /// Interaction logic for GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        public GenerateWindow(Campaign currentCampaign)
        {
            InitializeComponent();
            DataContext = currentCampaign;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
