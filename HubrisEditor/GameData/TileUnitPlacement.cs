using HubrisEditor.Core;
using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("TileUnitPlacement")]
    public class TileUnitPlacement : EditorComponentBase, IPostDeserializable
    {
        [XmlAttribute("Name")]
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

        [XmlIgnore()]
        public SolidColorBrush ColorBrush
        {
            get
            {
                return new SolidColorBrush(m_tileColor);
            }
        }

        [XmlElement("TileColor")]
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

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
        }

        private ProjectManager m_manager;
        private Color m_tileColor;
        private string m_name;
    }
}
