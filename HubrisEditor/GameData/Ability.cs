using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubrisEditor.GameData
{
    public class Ability : EditorComponentBase
    {
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

        private string m_description;
    }
}
