using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HubrisEditor.Core
{
    [XmlType("TileDataBase")]
    public class TileDataBase : EditorComponentBase, IPostDeserializable
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

        public virtual void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
        }

        protected ProjectManager m_manager;
        protected Color m_tileColor;
        protected string m_name;
    }
}
