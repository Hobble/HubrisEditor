using HubrisEditor.Core;
using HubrisEditor.ProjectIO;
using HubrisEditor.Xaml.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("TileSlot")]
    public class TileSlot : EditorComponentBase, IPostDeserializable
    {
        static TileSlot()
        {
            s_elevationBrushes = new List<SolidColorBrush>();
            s_maxElevation = 8;
            s_minElevation = 0;

            byte value = 0;
            for (int i = 0; i <= s_maxElevation; i++)
            {
                s_elevationBrushes.Add(new SolidColorBrush(new Color() { A = 255, R = value, G = value, B = value }));
                value += 31;
            }

            s_placementBrushes = new List<SolidColorBrush>();
            s_placementBrushes.Add(Brushes.LightGray);
            s_placementBrushes.Add(Brushes.Blue);
            s_placementBrushes.Add(Brushes.LightBlue);
            s_placementBrushes.Add(Brushes.Red);
            s_placementBrushes.Add(Brushes.Maroon);
            s_placementBrushes.Add(Brushes.OrangeRed);
            s_placementBrushes.Add(Brushes.Green);
            s_placementBrushes.Add(Brushes.LightGreen);
            s_placementBrushes.Add(Brushes.Gold);
            s_placementBrushes.Add(Brushes.PaleGoldenrod);
            s_placementBrushes.Add(Brushes.Purple);
            s_placementBrushes.Add(Brushes.Magenta);
        }

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

        [XmlAttribute("TileElevation")]
        public int TileElevation
        {
            get
            {
                return m_tileElevation;
            }
            set
            {
                m_tileElevation = Math.Max(Math.Min(value, s_maxElevation), s_minElevation);
                NotifyPropertyChanged("TileElevation");
                NotifyPropertyChanged("ElevationBrush");
            }
        }

        [XmlIgnore()]
        public SolidColorBrush ElevationBrush
        {
            get
            {
                return s_elevationBrushes[m_tileElevation];
            }
        }

        [XmlAttribute("TileContentEnum")]
        public int TileContentEnum
        {
            get
            {
                return m_tileContentEnum;
            }
            set
            {
                m_tileContentEnum = Math.Min(Math.Max(value, 0), s_placementBrushes.Count - 1);
                NotifyPropertyChanged("TileContentEnum");
                NotifyPropertyChanged("ContentBrush");
            }
        }

        [XmlIgnore()]
        public SolidColorBrush ContentBrush
        {
            get
            {
                return s_placementBrushes[m_tileContentEnum];
            }
        }

        [XmlAttribute("IsInGameSpace")]
        public bool IsInGameSpace
        {
            get
            {
                return m_isInGameSpace;
            }
            set
            {
                m_isInGameSpace = value;
                NotifyPropertyChanged("IsInGameSpace");
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            m_initialized = true;
            foreach (var tileType in m_manager.CurrentCampaign.TileTypes)
            {
                if (tileType.Name.Equals(m_tileTypeKey))
                {
                    Tile = tileType;
                    break;
                }
            }
        }

        public void SetTileToCurrentlySelected()
        {
            if (m_manager.DefaultTile != null)
            {
                TileTypeKey = m_manager.DefaultTile.Name;
            }
        }

        private ProjectManager m_manager;
        private bool m_initialized;
        private string m_tileTypeKey;
        private TileType m_tile;
        private bool m_isInGameSpace;
        private int m_tileElevation = 4;
        private int m_tileContentEnum;
        private static List<SolidColorBrush> s_elevationBrushes;
        private static int s_maxElevation;
        private static int s_minElevation;
        private static List<SolidColorBrush> s_placementBrushes;
    }
}
