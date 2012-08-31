using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HubrisEditor.EffectLibrary
{
    public class HueField : ShaderEffect
    {
        public HueField()
        {
            PixelShader = new PixelShader();
            PixelShader.UriSource = new Uri("pack://application:,,,/Resources/HueField.ps");
        }
    }
}
