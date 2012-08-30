﻿using HubrisEditor.Core;
using HubrisEditor.GameData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.ProjectIO
{
    public class ProjectManager : EditorComponentBase
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

        public string CurrentPath
        {
            get
            {
                return m_currentPath;
            }
            set
            {
                m_currentPath = value;
                NotifyPropertyChanged("CurrentPath");
            }
        }

        public bool IsProjectLoaded
        {
            get
            {
                return m_isProjectLoaded;
            }
            set
            {
                m_isProjectLoaded = value;
                NotifyPropertyChanged("IsProjectLoaded");
            }
        }

        public void NewProject(string name, string scenario)
        {
            CurrentCampaign = new Campaign(name, scenario);
            IsProjectLoaded = true;
        }

        public void OpenProject(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
            CurrentCampaign = serializer.Deserialize(fs) as Campaign;
            m_currentPath = path;
            IsProjectLoaded = true;

        }

        public void SaveProject()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
            TextWriter writer = new StreamWriter(m_currentPath);
            serializer.Serialize(writer, m_currentCampaign);
        }

        public void SaveProjectAs(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, m_currentCampaign);
            m_currentPath = path;
        }

        private Campaign m_currentCampaign;
        private string m_currentPath;
        private bool m_isProjectLoaded;
    }
}
