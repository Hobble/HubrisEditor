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
    [XmlRoot("Campaign")]
    public class Campaign : EditorComponentBase, IPostDeserializable
    {
        public Campaign()
        {
            InitializeMembers();
        }

        public Campaign(string name)
        {
            InitializeMembers();
            Name = name;
        }

        public Campaign(string name, string scenario)
        {
            InitializeMembers();
            Name = name;
            Scenarios.Add(new Scenario() { Name = scenario });
        }

        private void InitializeMembers()
        {
            m_scenarios = new ObservableCollection<Scenario>();
            m_tileTypes = new ObservableCollection<TileType>();
            m_tileUnitPlacements = new ObservableCollection<TileUnitPlacement>();
            m_tileContents = new ObservableCollection<TileContent>();
            m_abilities = new ObservableCollection<Ability>();
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            if (TileTypes == null)
            {
                TileTypes = new ObservableCollection<TileType>();
            }
            foreach (var tileType in TileTypes)
            {
                tileType.PostDeserialize(sender);
            }
            if (TileUnitPlacements == null)
            {
                TileUnitPlacements = new ObservableCollection<TileUnitPlacement>();
            }
            foreach (var tileUnitPlacement in TileUnitPlacements)
            {
                tileUnitPlacement.PostDeserialize(sender);
            }
            if (TileContents == null)
            {
                TileContents = new ObservableCollection<TileContent>();
            }
            foreach (var tileContent in TileContents)
            {
                tileContent.PostDeserialize(sender);
            }
            if (Scenarios == null)
            {
                Scenarios = new ObservableCollection<Scenario>();
            }
            foreach (var scenario in Scenarios)
            {
                scenario.PostDeserialize(sender);
            }
            if (Abilities == null)
            {
                Abilities = new ObservableCollection<Ability>();
            }
            foreach (var ability in Abilities)
            {
                ability.PostDeserialize(sender);
            }
            SortTileTypes();
        }

        [XmlArray("Scenarios")]
        public ObservableCollection<Scenario> Scenarios
        {
            get
            {
                return m_scenarios;
            }
            set
            {
                m_scenarios = value;
                NotifyPropertyChanged("Scenarios");
            }
        }

        [XmlArray("TileTypes")]
        public ObservableCollection<TileType> TileTypes
        {
            get
            {
                return m_tileTypes;
            }
            set
            {
                m_tileTypes = value;
                NotifyPropertyChanged("TileTypes");
            }
        }

        [XmlArray("TileUnitPlacements")]
        public ObservableCollection<TileUnitPlacement> TileUnitPlacements
        {
            get
            {
                return m_tileUnitPlacements;
            }
            set
            {
                m_tileUnitPlacements = value;
                NotifyPropertyChanged("TileUnitPlacements");
            }
        }

        [XmlArray("TileContents")]
        public ObservableCollection<TileContent> TileContents
        {
            get
            {
                return m_tileContents;
            }
            set
            {
                m_tileContents = value;
                NotifyPropertyChanged("TileContents");
            }
        }

        [XmlArray("Abilities")]
        public ObservableCollection<Ability> Abilities
        {
            get
            {
                return m_abilities;
            }
            set
            {
                m_abilities = value;
                NotifyPropertyChanged("Abilities");
            }
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

        public void SortTileTypes()
        {
            List<TileType> types = new List<TileType>();
            foreach (var type in m_tileTypes)
            {
                types.Add(type);
            }
            types.Sort(new Comparison<TileType>((TileType a, TileType b) => a.Name.CompareTo(b.Name)));
            m_tileTypes.Clear();
            foreach (var type in types)
            {
                m_tileTypes.Add(type);
            }
        }

        #region Members
        private ObservableCollection<Scenario> m_scenarios;
        private ObservableCollection<TileType> m_tileTypes;
        private ObservableCollection<TileUnitPlacement> m_tileUnitPlacements;
        private ObservableCollection<TileContent> m_tileContents;
        private ObservableCollection<Ability> m_abilities;
        private string m_name;
        private ProjectManager m_manager;
        #endregion
    }
}
