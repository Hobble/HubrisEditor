using HubrisEditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HubrisEditor.GameData
{
    [XmlType("Scenario")]
    public class Scenario : EditorComponentBase, IPostDeserializable
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

        [XmlAttribute("CanvasSpaceWidth")]
        public int CanvasSpaceWidth
        {
            get
            {
                return m_canvasSpaceWidth;
            }
            set
            {
                m_canvasSpaceWidth = value;
                NotifyPropertyChanged("CanvasSpaceWidth");
            }
        }

        [XmlAttribute("CanvasSpaceHeight")]
        public int CanvasSpaceHeight
        {
            get
            {
                return m_canvasSpaceHeight;
            }
            set
            {
                m_canvasSpaceHeight = value;
                NotifyPropertyChanged("CanvasSpaceHeight");
            }
        }

        [XmlAttribute("GameSpaceWidth")]
        public int GameSpaceWidth
        {
            get
            {
                return m_gameSpaceWidth;
            }
            set
            {
                m_gameSpaceWidth = value;
                NotifyPropertyChanged("GameSpaceWidth");
            }
        }

        [XmlAttribute("GameSpaceHeight")]
        public int GameSpaceHeight
        {
            get
            {
                return m_gameSpaceHeight;
            }
            set
            {
                m_gameSpaceHeight = value;
                NotifyPropertyChanged("GameSpaceHeight");
            }
        }

        [XmlAttribute("GameSpaceOffsetX")]
        public int GameSpaceOffsetX
        {
            get
            {
                return m_gameSpaceOffsetX;
            }
            set
            {
                m_gameSpaceOffsetX = value;
                NotifyPropertyChanged("GameSpaceOffsetX");
            }
        }

        [XmlAttribute("GameSpaceOffsetY")]
        public int GameSpaceOffsetY
        {
            get
            {
                return m_gameSpaceOffsetY;
            }
            set
            {
                m_gameSpaceOffsetY = value;
                NotifyPropertyChanged("GameSpaceOffsetY");
            }
        }

        public void PostDeserialize()
        {
        }

        #region Members
        private string m_name;
        private int m_canvasSpaceWidth;
        private int m_canvasSpaceHeight;
        private int m_gameSpaceWidth;
        private int m_gameSpaceHeight;
        private int m_gameSpaceOffsetX;
        private int m_gameSpaceOffsetY;
        #endregion
    }
}
