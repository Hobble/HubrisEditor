using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HubrisEditor.EffectLibrary
{
    public class GreenRange : ShaderEffect
    {
        public GreenRange()
        {
            PixelShader = new PixelShader();
            PixelShader.UriSource = new Uri("pack://application:,,,/Resources/GreenRange.ps");
            UpdateShaderValue(RedProperty);
            UpdateShaderValue(BlueProperty);
        }

        public double Red
        {
            get
            {
                return ((double)(this.GetValue(RedProperty)));
            }
            set
            {
                this.SetValue(RedProperty, value);
            }
        }

        public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(double), typeof(GreenRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

        public double Blue
        {
            get
            {
                return ((double)(this.GetValue(BlueProperty)));
            }
            set
            {
                this.SetValue(BlueProperty, value);
            }
        }

        public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(double), typeof(GreenRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));

    }
}
