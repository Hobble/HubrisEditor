using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("TileContent")]
    public class TileContent : TileDataBase
    {
        [XmlAttribute("IsConsumed")]
        public bool IsConsumed
        {
            get
            {
                return m_isConsumed;
            }
            set
            {
                m_isConsumed = value;
                NotifyPropertyChanged("IsConsumed");
            }
        }

        [XmlAttribute("EffectsAdjacent")]
        public bool EffectsAdjacent
        {
            get
            {
                return m_effectsAdjacent;
            }
            set
            {
                m_effectsAdjacent = value;
                NotifyPropertyChanged("EffectsAdjacent");
            }
        }

        private bool m_isConsumed;
        private bool m_effectsAdjacent;
    }
}
