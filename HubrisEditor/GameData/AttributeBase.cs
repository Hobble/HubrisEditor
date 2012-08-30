using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("Attribute")]
    public class AttributeBase : EditorComponentBase, IPostDeserializable
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

        [XmlAttribute("Type")]
        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                NotifyPropertyChanged("Type");
            }
        }

        [XmlElement()]
        public string Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                NotifyPropertyChanged("Value");
            }
        }

        public void PostDeserialize()
        {
        }

        private string m_name;
        private string m_type;
        private string m_value;
    }
}
