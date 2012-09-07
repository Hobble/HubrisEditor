﻿using HubrisEditor.Core;
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
            s_placementBrushes.Add(Brushes.Brown);
            s_placementBrushes.Add(Brushes.LimeGreen);
            s_placementBrushes.Add(Brushes.CadetBlue);
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

        [XmlIgnore()]
        public TileUnitPlacement UnitPlacement
        {
            get
            {
                return m_unitPlacement;
            }
            private set
            {
                m_unitPlacement = value;
                NotifyPropertyChanged("UnitPlacement");
            }
        }

        [XmlAttribute("UnitPlacementKey")]
        public string UnitPlacementKey
        {
            get
            {
                return m_unitPlacementKey;
            }
            set
            {
                m_unitPlacementKey = value;
                NotifyPropertyChanged("UnitPlacementKey");
                if (m_initialized)
                {
                    foreach (var unitPlacement in m_manager.CurrentCampaign.TileUnitPlacements)
                    {
                        if (unitPlacement.Name.Equals(m_unitPlacementKey))
                        {
                            UnitPlacement = unitPlacement;
                            break;
                        }
                    }
                }
            }
        }

        [XmlIgnore()]
        public TileContent Content
        {
            get
            {
                return m_tileContent;
            }
            private set
            {
                m_tileContent = value;
                NotifyPropertyChanged("Content");
            }
        }

        [XmlAttribute("TileContentKey")]
        public string TileContentKey
        {
            get
            {
                return m_tileContentKey;
            }
            set
            {
                m_tileContentKey = value;
                NotifyPropertyChanged("TileContentKey");
                if (m_initialized)
                {
                    foreach (var tileContent in m_manager.CurrentCampaign.TileContents)
                    {
                        if (tileContent.Name.Equals(m_tileContentKey))
                        {
                            Content = tileContent;
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
            if (m_unitPlacementKey == null || m_unitPlacementKey == string.Empty)
            {
                if (m_tileContentEnum < m_manager.CurrentCampaign.TileUnitPlacements.Count)
                {
                    m_unitPlacementKey = m_manager.CurrentCampaign.TileUnitPlacements[m_tileContentEnum].Name;
                }
                else
                {
                    m_unitPlacementKey = m_manager.CurrentCampaign.TileUnitPlacements[0].Name;
                }
            }
            if (m_tileContentKey == null || m_tileContentKey == string.Empty)
            {
                if (m_tileContentEnum > (m_manager.CurrentCampaign.TileUnitPlacements.Count - 1) && m_tileContentEnum < m_manager.CurrentCampaign.TileUnitPlacements.Count - 1 + m_manager.CurrentCampaign.TileContents.Count)
                {
                    m_tileContentKey = m_manager.CurrentCampaign.TileContents[m_tileContentEnum - (m_manager.CurrentCampaign.TileUnitPlacements.Count - 1)].Name;
                }
                else
                {
                    m_tileContentKey = m_manager.CurrentCampaign.TileUnitPlacements[0].Name;
                }
            }
            foreach (var tileType in m_manager.CurrentCampaign.TileTypes)
            {
                if (tileType.Name.Equals(m_tileTypeKey))
                {
                    Tile = tileType;
                    break;
                }
            }
            foreach (var unitPlacement in m_manager.CurrentCampaign.TileUnitPlacements)
            {
                if (unitPlacement.Name.Equals(m_unitPlacementKey))
                {
                    UnitPlacement = unitPlacement;
                    break;
                }
            }
            foreach (var tileContent in m_manager.CurrentCampaign.TileContents)
            {
                if (tileContent.Name.Equals(m_tileContentKey))
                {
                    Content = tileContent;
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
        private string m_unitPlacementKey;
        private TileUnitPlacement m_unitPlacement;
        private string m_tileContentKey;
        private TileContent m_tileContent;
        private bool m_isInGameSpace;
        private int m_tileElevation = 4;
        private int m_tileContentEnum;
        private static List<SolidColorBrush> s_elevationBrushes;
        private static int s_maxElevation;
        private static int s_minElevation;
        private static List<SolidColorBrush> s_placementBrushes;
    }
}
