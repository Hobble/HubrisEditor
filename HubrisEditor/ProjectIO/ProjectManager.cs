using HubrisEditor.Core;
using HubrisEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubrisEditor.ProjectIO
{
    public class ProjectManager : GameComponentRoot
    {
        public Campaign CurrentCampaign
        {
            get
            {
                return m_currentCampaign;
            }
            set
            {
                m_currentCampaign = value;
                NotifyPropertyChanged("CurrentCampaign");
            }
        }

        public void NewProject(string name, string scenario)
        {
            CurrentCampaign = new Campaign(name, scenario);
        }

        public void OpenProject(string path)
        {
        }

        public void SaveProject()
        {
        }

        public void SaveProjectAs(string path)
        {
        }

        private Campaign m_currentCampaign;
    }
}
