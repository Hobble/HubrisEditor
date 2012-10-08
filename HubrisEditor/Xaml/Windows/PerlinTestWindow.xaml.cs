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

            WriteableBitmap wb = new WriteableBitmap(512, 512, 96.0, 96.0, PixelFormats.Gray8, null);
            byte[] pixels = new byte[512 * 512];
            PerlinNoise noise = new PerlinNoise(99);
            float persistence = 0.5f;
            float min = float.MaxValue;
            float max = float.MinValue;
            for(int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    float k = 0.0f;
                    float result = 0.0f;
                    do
                    {
                        float frequency = (float)Math.Pow(2.0, (float)k + 2.0f);
                        float amplitude = (float)Math.Pow(persistence, (float)k + 1.0f);
                        float value = noise.Noise((frequency * i) / 512.0f, (frequency * j) / 512.0f, 0.0f);
                        value = Math.Max(Math.Min(value + 0.5f, 1.0f), 0.0f);
                        result += (value * amplitude);
                        k += 1;
                    } while (k < 6);
                    if (result > max)
                    {
                        max = result;
                    }
                    if (result < min)
                    {
                        min = result;
                    }
                    pixels[i * 512 + j] = (byte)(result * 255);
                }
            }

            wb.WritePixels(new Int32Rect(0, 0, 512, 512), pixels, 512, 0);
            MainImage.Source = wb;
        }
    }
}
