using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HubrisEditor.EffectLibrary
{
    public class BlueRange : ShaderEffect
    {
        public BlueRange()
        {
            PixelShader = new PixelShader();
            PixelShader.UriSource = new Uri("pack://application:,,,/Resources/BlueRange.ps");
            UpdateShaderValue(GreenProperty);
            UpdateShaderValue(RedProperty);
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

        public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(double), typeof(BlueRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

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

        public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(double), typeof(BlueRange), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
    }
}
