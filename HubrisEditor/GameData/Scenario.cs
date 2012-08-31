using HubrisEditor.Core;
using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("Scenario")]
    public class Scenario : EditorComponentBase, IPostDeserializable
    {
        public Scenario()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            m_tileSlots = new ObservableCollection<TileSlot>();
        }

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

        [XmlAttribute("CanvasSpaceWidth")]
        public int CanvasSpaceWidth
        {
            get
            {
                return m_canvasSpaceWidth;
            }
            set
            {
                m_canvasSpaceWidth = value;
                NotifyPropertyChanged("CanvasSpaceWidth");
            }
        }

        [XmlAttribute("CanvasSpaceHeight")]
        public int CanvasSpaceHeight
        {
            get
            {
                return m_canvasSpaceHeight;
            }
            set
            {
                m_canvasSpaceHeight = value;
                NotifyPropertyChanged("CanvasSpaceHeight");
            }
        }

        [XmlAttribute("GameSpaceWidth")]
        public int GameSpaceWidth
        {
            get
            {
                return m_gameSpaceWidth;
            }
            set
            {
                m_gameSpaceWidth = value;
                NotifyPropertyChanged("GameSpaceWidth");
            }
        }

        [XmlAttribute("GameSpaceHeight")]
        public int GameSpaceHeight
        {
            get
            {
                return m_gameSpaceHeight;
            }
            set
            {
                m_gameSpaceHeight = value;
                NotifyPropertyChanged("GameSpaceHeight");
            }
        }

        [XmlAttribute("GameSpaceOffsetX")]
        public int GameSpaceOffsetX
        {
            get
            {
                return m_gameSpaceOffsetX;
            }
            set
            {
                m_gameSpaceOffsetX = value;
                NotifyPropertyChanged("GameSpaceOffsetX");
            }
        }

        [XmlAttribute("GameSpaceOffsetY")]
        public int GameSpaceOffsetY
        {
            get
            {
                return m_gameSpaceOffsetY;
            }
            set
            {
                m_gameSpaceOffsetY = value;
                NotifyPropertyChanged("GameSpaceOffsetY");
            }
        }

        [XmlIgnore()]
        public double TileSlotGridHeight
        {
            get
            {
                return m_canvasSpaceHeight * 65.0;
            }
        }

        [XmlIgnore()]
        public double TileSlotGridWidth
        {
            get
            {
                return m_canvasSpaceWidth * 65.0;
            }
        }

        [XmlArray("TileSlots")]
        public ObservableCollection<TileSlot> TileSlots
        {
            get
            {
                return m_tileSlots;
            }
            set
            {
                m_tileSlots = value;
                NotifyPropertyChanged("TileSlots");
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            if (TileSlots == null)
            {
                TileSlots = new ObservableCollection<TileSlot>();
            }
            foreach (var tileSlot in TileSlots)
            {
                tileSlot.PostDeserialize(sender);
            }
            NotifyPropertyChanged("TileSlotGridHeight");
            NotifyPropertyChanged("TileSlotGridWidth");
        }

        public void GenerateTiles()
        {
            NotifyPropertyChanged("TileSlotGridHeight");
            NotifyPropertyChanged("TileSlotGridWidth");
            m_tileSlots.Clear();
            TileType defaultTile = m_manager.DefaultTile;
            if (defaultTile == null)
            {
                defaultTile = m_manager.CurrentCampaign.TileTypes.FirstOrDefault<TileType>();
                if (defaultTile == null)
                {
                    return;
                }
            }
            int total = m_canvasSpaceHeight * m_canvasSpaceWidth;
            for (int i = 0; i < total; i++)
            {
                TileSlot slot = new TileSlot();
                slot.PostDeserialize(m_manager);
                slot.TileTypeKey = defaultTile.Name;
                m_tileSlots.Add(slot);
            }
        }

        public void UpdateOffsets()
        {
        }

        #region Members
        private ProjectManager m_manager;
        private ObservableCollection<TileSlot> m_tileSlots;
        private string m_name;
        private int m_canvasSpaceWidth;
        private int m_canvasSpaceHeight;
        private int m_gameSpaceWidth;
        private int m_gameSpaceHeight;
        private int m_gameSpaceOffsetX;
        private int m_gameSpaceOffsetY;
        #endregion
    }
}
