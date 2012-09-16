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

        [XmlAttribute("GameSpacePadding")]
        public int GameSpacePadding
        {
            get
            {
                return m_gameSpacePadding;
            }
            set
            {
                m_gameSpacePadding = value;
                NotifyPropertyChanged("GameSpacePadding");
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
            UpdateOffsets();
            RaiseTilesGeneratedEvent();
        }

        public void UpdateOffsets()
        {
            int i = 0;
            foreach (var slot in TileSlots)
            {
                int x = i % CanvasSpaceWidth;
                int y = i / CanvasSpaceWidth;
                int minX = m_gameSpacePadding;
                int minY = m_gameSpacePadding;
                int maxX = CanvasSpaceWidth - m_gameSpacePadding;
                int maxY = CanvasSpaceHeight - m_gameSpacePadding;
                if (x >= minX && x < maxX && y >= minY && y < maxY)
                {
                    slot.IsInGameSpace = true;
                }
                i++;
            }
        }

        #region Events
        public event EventHandler TilesGenerated;

        private void RaiseTilesGeneratedEvent()
        {
            if (TilesGenerated != null)
            {
                TilesGenerated(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Members
        private ProjectManager m_manager;
        private ObservableCollection<TileSlot> m_tileSlots;
        private string m_name;
        private int m_canvasSpaceWidth;
        private int m_canvasSpaceHeight;
        private int m_gameSpacePadding;
        #endregion
    }
}
