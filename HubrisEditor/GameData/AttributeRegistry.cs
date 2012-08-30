using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("Registry")]
    public class AttributeRegistry : EditorComponentBase, IPostDeserializable
    {
        [XmlArray("TileTypeAttributes")]
        public ObservableCollection<AttributeBase> TileTypeAttributes
        {
            get
            {
                return m_tileTypeAttributes;
            }
            set
            {
                m_tileTypeAttributes = value;
                NotifyPropertyChanged("TileTypeAttributes");
            }
        }

        [XmlArray("ScenarioAttributes")]
        public ObservableCollection<AttributeBase> ScenarioAttributes
        {
            get
            {
                return m_scenarioAttributes;
            }
            set
            {
                m_scenarioAttributes = value;
                NotifyPropertyChanged("ScenarioAttributes");
            }
        }

        [XmlArray("CampaignAttributes")]
        public ObservableCollection<AttributeBase> CampaignAttributes
        {
            get
            {
                return m_campaignAttributes;
            }
            set
            {
                m_campaignAttributes = value;
                NotifyPropertyChanged("CampaignAttributes");
            }
        }

        public void PostDeserialize()
        {
            foreach (var attribute in TileTypeAttributes)
            {
                attribute.PostDeserialize();
            }

            foreach (var attribute in ScenarioAttributes)
            {
                attribute.PostDeserialize();
            }

            foreach (var attribute in CampaignAttributes)
            {
                attribute.PostDeserialize();
            }
        }

        private ObservableCollection<AttributeBase> m_tileTypeAttributes;
        private ObservableCollection<AttributeBase> m_scenarioAttributes;
        private ObservableCollection<AttributeBase> m_campaignAttributes;
    }
}
