using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HubrisEditor.EffectLibrary
{
    public class SVField : ShaderEffect
    {
        public SVField()
        {
            PixelShader = new PixelShader();
            PixelShader.UriSource = new Uri("pack://application:,,,/Resources/SVField.ps");
            UpdateShaderValue(HueProperty);
        }

        public double Hue
        {
            get
            {
                return ((double)(this.GetValue(HueProperty)));
            }
            set
            {
                this.SetValue(HueProperty, value);
            }
        }

        public static readonly DependencyProperty HueProperty = DependencyProperty.Register("Hue", typeof(double), typeof(SVField), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    }
}
