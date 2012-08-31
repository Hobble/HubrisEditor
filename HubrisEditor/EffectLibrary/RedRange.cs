using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HubrisEditor.EffectLibrary
{
    public class RedRange : ShaderEffect
    {
        public RedRange()
        {
            PixelShader = new PixelShader();
            PixelShader.UriSource = new Uri("pack://application:,,,/Resources/RedRange.ps");
            UpdateShaderValue(GreenProperty);
            UpdateShaderValue(BlueProperty);
        }

        public double Green
        {
            get
            {
                return ((double)(this.GetValue(GreenProperty)));
            }
            set
            {
                this.SetValue(GreenProperty, value);
            }
        }

        public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(double), typeof(RedRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

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

        public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(double), typeof(RedRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
    }
}
