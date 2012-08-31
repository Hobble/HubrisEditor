using HubrisEditor.Core;
using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("TileSlot")]
    public class TileSlot : EditorComponentBase, IPostDeserializable
    {
        [XmlIgnore()]
        public TileType Tile
        {
            get
            {
                return m_tile;
            }
            private set
            {
                m_tile = value;
                NotifyPropertyChanged("Tile");
            }
        }

        [XmlAttribute("TileTypeKey")]
        public string TileTypeKey
        {
            get
            {
                return m_tileTypeKey;
            }
            set
            {
                m_tileTypeKey = value;
                NotifyPropertyChanged("TileTypeKey");
                if (m_initialized)
                {
                    foreach (var tileType in m_manager.CurrentCampaign.TileTypes)
                    {
                        if (tileType.Name.Equals(m_tileTypeKey))
                        {
                            Tile = tileType;
                            break;
                        }
                    }
                }
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            m_initialized = true;
        }

        private ProjectManager m_manager;
        private bool m_initialized;
        private string m_tileTypeKey;
        private TileType m_tile;
    }
}
