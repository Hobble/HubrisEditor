using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Controls.Primitives;

namespace HubrisEditor.Xaml.UserControls
{
    /// <summary>
    /// Interaction logic for HSVColorSwatch.xaml
    /// </summary>
    public partial class HSVColorSwatch : UserControl, INotifyPropertyChanged
    {
        public HSVColorSwatch()
        {
            InitializeComponent();
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            m_color = new Color();
            m_result = new SolidColorBrush(m_color);
            m_color.R = (byte)255;
            m_color.G = (byte)255;
            m_color.B = (byte)255;
            m_color.A = (byte)255;
            m_red = (byte)255;
            m_green = (byte)255;
            m_blue = (byte)255;
            m_alpha = (byte)255;
            m_hue = 0.0;
            m_saturation = 1.0;
            m_value = 1.0;
        }

        public SolidColorBrush Result
        {
            get
            {
                return m_result;
            }
        }

        public byte Red
        {
            get
            {
                return m_red;
            }
            set
            {
                m_red = value;
                NotifyPropertyChanged("Red");
                ComputeHSV();
                m_color.R = m_red;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public byte Green
        {
            get
            {
                return m_green;
            }
            set
            {
                m_green = value;
                NotifyPropertyChanged("Green");
                ComputeHSV();
                m_color.G = m_green;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public byte Blue
        {
            get
            {
                return m_blue;
            }
            set
            {
                m_blue = value;
                NotifyPropertyChanged("Blue");
                ComputeHSV();
                m_color.B = m_blue;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public byte Alpha
        {
            get
            {
                return m_alpha;
            }
            set
            {
                m_alpha = value;
                NotifyPropertyChanged("Alpha");
                m_color.A = m_alpha;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public double Hue
        {
            get
            {
                return m_hue;
            }
            set
            {
                m_hue = value;
                NotifyPropertyChanged("Hue");
                ComputeRGB();
                m_color.R = m_red;
                m_color.G = m_green;
                m_color.B = m_blue;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public double Saturation
        {
            get
            {
                return m_saturation;
            }
            set
            {
                m_saturation = value;
                NotifyPropertyChanged("Saturation");
                ComputeRGB();
                m_color.R = m_red;
                m_color.G = m_green;
                m_color.B = m_blue;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        public double Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                NotifyPropertyChanged("Value");
                ComputeRGB();
                m_color.R = m_red;
                m_color.G = m_green;
                m_color.B = m_blue;
                m_result.Color = m_color;
                NotifyPropertyChanged("Result");
            }
        }

        private void ComputeHSV()
        {
            double min, max, delta;
            min = Math.Min(m_red, Math.Min(m_green, m_blue));
            max = Math.Max(m_red, Math.Max(m_green, m_blue));
            m_value = max / 255.0;
            delta = max - min;
            if (delta != 0)
            {
                m_saturation = delta / max;
            }
            else
            {
                m_saturation = 0;
                m_hue = 0;
                NotifyPropertyChanged("Hue");
                NotifyPropertyChanged("Saturation");
                NotifyPropertyChanged("Value");
                return;
            }
            if (m_red == max)
            {
                m_hue = (m_green - m_blue) / delta;
            }
            else if (m_green == max)
            {
                m_hue = 2 + (m_blue - m_red) / delta;
            }
            else
            {
                m_hue = 4 + (m_red - m_green) / delta;
            }
            m_hue *= 60;
            if (m_hue < 0)
            {
                m_hue += 360;
            }
            NotifyPropertyChanged("Hue");
            NotifyPropertyChanged("Saturation");
            NotifyPropertyChanged("Value");
        }

        private void ComputeRGB()
        {
            double v = m_value * 255.0;
            double h = m_hue;
            if (m_saturation == 0.0)
            {
                m_red = (byte)v;
                m_green = (byte)v;
                m_blue = (byte)v;
                NotifyPropertyChanged("Red");
                NotifyPropertyChanged("Green");
                NotifyPropertyChanged("Blue");
                return;
            }
            h = h / 60.0;
            double C = v * m_saturation;
            double X = C * (1 - Math.Abs((h % 2) - 1));
            double m = v - C;
            if (0 <= h && h < 1)
            {
                m_red = (byte)(C + m);
                m_green = (byte)(X + m);
                m_blue = (byte)(0 + m);
            }
            else if (1 <= h && h < 2)
            {
                m_red = (byte)(X + m);
                m_green = (byte)(C + m);
                m_blue = (byte)(0 + m);
            }
            else if (2 <= h && h < 3)
            {
                m_red = (byte)(0 + m);
                m_green = (byte)(C + m);
                m_blue = (byte)(X + m);
            }
            else if (3 <= h && h < 4)
            {
                m_red = (byte)(0 + m);
                m_green = (byte)(X + m);
                m_blue = (byte)(C + m);
            }
            else if (4 <= h && h < 5)
            {
                m_red = (byte)(X + m);
                m_green = (byte)(0 + m);
                m_blue = (byte)(C + m);
            }
            else if (5 <= h && h < 6)
            {
                m_red = (byte)(C + m);
                m_green = (byte)(0 + m);
                m_blue = (byte)(X + m);
            }
            NotifyPropertyChanged("Red");
            NotifyPropertyChanged("Green");
            NotifyPropertyChanged("Blue");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RedThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double delta = (e.HorizontalChange / RedRangeField.ActualWidth) * 255.0;
            double result = Red + delta;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Red = (byte)clamped;
        }

        private void GreenThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double delta = (e.HorizontalChange / RedRangeField.ActualWidth) * 255.0;
            double result = Green + delta;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Green = (byte)clamped;
        }

        private void BlueThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double delta = (e.HorizontalChange / RedRangeField.ActualWidth) * 255.0;
            double result = Blue + delta;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Blue = (byte)clamped;
        }

        private void AlphaThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double delta = (e.HorizontalChange / RedRangeField.ActualWidth) * 255.0;
            double result = Alpha + delta;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Alpha = (byte)clamped;
        }

        private void HueThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double delta = (e.VerticalChange / HueFieldCanvas.ActualHeight) * 360.0;
            double result = Hue + delta;
            double clamped = Math.Max(Math.Min(result, 360.0), 0.0);
            Hue = clamped;
        }

        private void SVThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double dv = e.VerticalChange / SVFieldCanvas.ActualHeight;
            double resultv = Value + dv;
            double clampedv = Math.Max(Math.Min(resultv, 1.0), 0.0);
            Value = clampedv;
            double ds = e.HorizontalChange / SVFieldCanvas.ActualWidth;
            double results = Saturation + ds;
            double clampeds = Math.Max(Math.Min(results, 1.0), 0.0);
            Saturation = clampeds;
        }

        private void RedThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(RedThumbCanvas);
            double result = (point.X / RedThumbCanvas.ActualWidth) * 255.0;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Red = (byte)clamped;
            Dispatcher.BeginInvoke(new Action(() => RedThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private void GreenThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(GreenThumbCanvas);
            double result = (point.X / GreenThumbCanvas.ActualWidth) * 255.0;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Green = (byte)clamped;
            Dispatcher.BeginInvoke(new Action(() => GreenThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private void BlueThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BlueThumbCanvas);
            double result = (point.X / BlueThumbCanvas.ActualWidth) * 255.0;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Blue = (byte)clamped;
            Dispatcher.BeginInvoke(new Action(() => BlueThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private void AlphaThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(AlphaThumbCanvas);
            double result = (point.X / AlphaThumbCanvas.ActualWidth) * 255.0;
            double clamped = Math.Max(Math.Min(result, 255.0), 0.0);
            Alpha = (byte)clamped;
            Dispatcher.BeginInvoke(new Action(() => AlphaThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private void HueThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(HueThumbCanvas);
            double result = (point.Y / HueThumbCanvas.ActualHeight) * 360.0;
            double clamped = Math.Max(Math.Min(result, 360.0), 0.0);
            Hue = clamped;
            Dispatcher.BeginInvoke(new Action(() => HueThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private void SVThumbCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(SVThumbCanvas);
            double dv = point.Y / SVFieldCanvas.ActualHeight;
            double clampedv = Math.Max(Math.Min(dv, 1.0), 0.0);
            Value = clampedv;
            double ds = point.X / SVFieldCanvas.ActualWidth;
            double clampeds = Math.Max(Math.Min(ds, 1.0), 0.0);
            Saturation = clampeds;
            Dispatcher.BeginInvoke(new Action(() => SVThumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton) { RoutedEvent = Thumb.MouseDownEvent })), System.Windows.Threading.DispatcherPriority.Background, null);
            e.Handled = true;
        }

        private byte m_red;
        private byte m_green;
        private byte m_blue;
        private byte m_alpha;
        private double m_hue;
        private double m_saturation;
        private double m_value;
        private Color m_color;
        private SolidColorBrush m_result;
    }

    public class ByteToCanvasPositionMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                byte data = (byte)values[0];
                double height = (double)values[1];
                double width = (double)values[2];
                bool isVertical = string.Compare(parameter.ToString(), "vertical", true) == 0;

                if (isVertical)
                {
                    return (data / 255.0) * height;
                }
                else
                {
                    return (data / 255.0) * width;
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UnitDoubleToCanvasPositionMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double data = (double)values[0];
                double height = (double)values[1];
                double width = (double)values[2];
                bool isVertical = string.Compare(parameter.ToString(), "vertical", true) == 0;

                if (isVertical)
                {
                    return data * height;
                }
                else
                {
                    return data * width;
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HueToCanvasPositionMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double data = (double)values[0];
                double height = (double)values[1];
                double width = (double)values[2];
                bool isVertical = string.Compare(parameter.ToString(), "vertical", true) == 0;

                if (isVertical)
                {
                    return (data / 360.0) * height;
                }
                else
                {
                    return (data / 360.0) * width;
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RGBAByteToDoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                byte data = (byte)value;
                return data / 255.0;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RGBAByteToHexStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                byte red = (byte)values[0];
                byte green = (byte)values[1];
                byte blue = (byte)values[2];
                byte alpha = (byte)values[3];
                byte[] bytes = new byte[] { alpha, red, green, blue };

                string result = "#" + BitConverter.ToString(bytes).Replace("-", "");
                return result;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
