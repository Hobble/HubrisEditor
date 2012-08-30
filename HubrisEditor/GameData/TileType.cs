using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HubrisEditor.GameData
{
    public class TileType : EditorComponentBase
    {
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public SolidColorBrush ColorBrush
        {
            get
            {
                return new SolidColorBrush(m_tileColor);
            }
        }

        public Color TileColor
        {
            get
            {
                return m_tileColor;
            }
            set
            {
                m_tileColor = value;
                NotifyPropertyChanged("TileColor");
                NotifyPropertyChanged("ColorBrush");
            }
        }

        private Color m_tileColor;
        private string m_name;
    }
}
