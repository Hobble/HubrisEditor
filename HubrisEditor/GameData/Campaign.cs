using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubrisEditor.GameData
{
    public class Campaign : GameComponentRoot
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
        }

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
        private string m_name;
        #endregion
    }
}
