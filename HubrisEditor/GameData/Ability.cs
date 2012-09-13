using HubrisEditor.Core;
using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("Ability")]
    public class Ability : EditorComponentBase, IPostDeserializable
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

        [XmlAttribute("Description")]
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
        }

        private ProjectManager m_manager;
        private string m_name;
        private string m_description;
    }
}
