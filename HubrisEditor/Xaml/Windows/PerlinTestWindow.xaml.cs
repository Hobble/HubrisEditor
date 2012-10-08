using HubrisEditor.Core;
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
    /// Interaction logic for PerlinTestWindow.xaml
    /// </summary>
    public partial class PerlinTestWindow : Window
    {
        public PerlinTestWindow()
        {
            InitializeComponent();
            WriteableBitmap wb = new WriteableBitmap(256, 256, 96.0, 96.0, PixelFormats.Gray8, null);
            PerlinNoise noise = new PerlinNoise(99);
            wb.WritePixels(new Int32Rect(0, 0, 256, 256), noise.GetPixels(256, 256), 256, 0);
            MainImage.Source = wb;
        }
    }
}
