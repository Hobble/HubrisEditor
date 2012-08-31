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
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            if (Scenarios == null)
            {
                Scenarios = new ObservableCollection<Scenario>();
            }
            foreach (var scenario in Scenarios)
            {
                scenario.PostDeserialize(sender);
            }
            if (TileTypes == null)
            {
                TileTypes = new ObservableCollection<TileType>();
            }
            foreach (var tileType in TileTypes)
            {
                tileType.PostDeserialize(sender);
            }
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

        #region Members
        private ObservableCollection<Scenario> m_scenarios;
        private ObservableCollection<TileType> m_tileTypes;
        private string m_name;
        private ProjectManager m_manager;
        #endregion
    }
}
