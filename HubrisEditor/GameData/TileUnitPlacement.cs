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
    [XmlType("TileUnitPlacement")]
    public class TileUnitPlacement : TileDataBase
    {
        [XmlAttribute("IsVisibleInPreBattle")]
        public bool IsVisibleInPreBattle
        {
            get
            {
                return m_isVisibleInPreBattle;
            }
            set
            {
                m_isVisibleInPreBattle = value;
                NotifyPropertyChanged("IsVisibleInPreBattle");
            }
        }

        private bool m_isVisibleInPreBattle;
    }
}
