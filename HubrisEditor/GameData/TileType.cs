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
    [XmlType("TileType")]
    public class TileType : EditorComponentBase, IPostDeserializable
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

        [XmlAttribute("Avoidance")]
        public double Avoidance
        {
            get
            {
                return m_avoidance;
            }
            set
            {
                m_avoidance = value;
                NotifyPropertyChanged("Avoidance");
            }
        }

        [XmlAttribute("Resistance")]
        public double Resistance
        {
            get
            {
                return m_resistance;
            }
            set
            {
                m_resistance = value;
                NotifyPropertyChanged("Resistance");
            }
        }

        [XmlAttribute("Defense")]
        public double Defense
        {
            get
            {
                return m_defense;
            }
            set
            {
                m_defense = value;
                NotifyPropertyChanged("Defense");
            }
        }

        [XmlAttribute("HealthRegen")]
        public double HealthRegen
        {
            get
            {
                return m_healthRegen;
            }
            set
            {
                m_healthRegen = value;
                NotifyPropertyChanged("HealthRegen");
            }
        }

        [XmlAttribute("ManaRegen")]
        public double ManaRegen
        {
            get
            {
                return m_manaRegen;
            }
            set
            {
                m_manaRegen = value;
                NotifyPropertyChanged("ManaRegen");
            }
        }

        [XmlAttribute("EnergyRegen")]
        public double EnergyRegen
        {
            get
            {
                return m_energyRegen;
            }
            set
            {
                m_energyRegen = value;
                NotifyPropertyChanged("EnergyRegen");
            }
        }

        [XmlAttribute("TravelCost")]
        public double TravelCost
        {
            get
            {
                return m_travelCost;
            }
            set
            {
                m_travelCost = value;
                NotifyPropertyChanged("TravelCost");
            }
        }

        [XmlAttribute("HidesVision")]
        public bool HidesVision
        {
            get
            {
                return m_hidesVision;
            }
            set
            {
                m_hidesVision = value;
                NotifyPropertyChanged("HidesVision");
            }
        }

        [XmlAttribute("BlocksPassage")]
        public bool BlocksPassage
        {
            get
            {
                return m_blocksPassage;
            }
            set
            {
                m_blocksPassage = value;
                NotifyPropertyChanged("BlocksPassage");
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
        }

        private ProjectManager m_manager;
        private Color m_tileColor;
        private string m_name;
        private double m_avoidance;
        private double m_resistance;
        private double m_defense;
        private double m_healthRegen;
        private double m_manaRegen;
        private double m_energyRegen;
        private double m_travelCost = 1.0;
        private bool m_hidesVision;
        private bool m_blocksPassage;
    }
}
