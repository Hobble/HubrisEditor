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
    [XmlType("UnitClass")]
    public class UnitClass : EditorComponentBase, IPostDeserializable
    {
        [XmlAttribute("BaseHealth")]
        public double BaseHealth
        {
            get
            {
                return m_baseHealth;
            }
            set
            {
                m_baseHealth = value;
                NotifyPropertyChanged("BaseHealth");
            }
        }

        [XmlAttribute("HealthRate")]
        public double HealthRate
        {
            get
            {
                return m_healthRate;
            }
            set
            {
                m_healthRate = value;
                NotifyPropertyChanged("HealthRate");
            }
        }

        [XmlAttribute("BaseMana")]
        public double BaseMana
        {
            get
            {
                return m_baseMana;
            }
            set
            {
                m_baseMana = value;
                NotifyPropertyChanged("BaseMana");
            }
        }

        [XmlAttribute("ManaRate")]
        public double ManaRate
        {
            get
            {
                return m_manaRate;
            }
            set
            {
                m_manaRate = value;
                NotifyPropertyChanged("ManaRate");
            }
        }

        [XmlAttribute("BaseStrength")]
        public double BaseStrength
        {
            get
            {
                return m_baseStrength;
            }
            set
            {
                m_baseStrength = value;
                NotifyPropertyChanged("BaseStrength");
            }
        }

        [XmlAttribute("StrengthRate")]
        public double StrengthRate
        {
            get
            {
                return m_strengthRate;
            }
            set
            {
                m_strengthRate = value;
                NotifyPropertyChanged("StrengthRate");
            }
        }

        [XmlAttribute("BaseMagic")]
        public double BaseMagic
        {
            get
            {
                return m_baseMagic;
            }
            set
            {
                m_baseMagic = value;
                NotifyPropertyChanged("BaseMagic");
            }
        }

        [XmlAttribute("MagicRate")]
        public double MagicRate
        {
            get
            {
                return m_magicRate;
            }
            set
            {
                m_magicRate = value;
                NotifyPropertyChanged("MagicRate");
            }
        }

        [XmlAttribute("BaseSpeed")]
        public double BaseSpeed
        {
            get
            {
                return m_baseSpeed;
            }
            set
            {
                m_baseSpeed = value;
                NotifyPropertyChanged("BaseSpeed");
            }
        }

        [XmlAttribute("SpeedRate")]
        public double SpeedRate
        {
            get
            {
                return m_speedRate;
            }
            set
            {
                m_speedRate = value;
                NotifyPropertyChanged("SpeedRate");
            }
        }

        [XmlAttribute("BaseAccuracy")]
        public double BaseAccuracy
        {
            get
            {
                return m_baseAccuracy;
            }
            set
            {
                m_baseAccuracy = value;
                NotifyPropertyChanged("BaseAccuracy");
            }
        }

        [XmlAttribute("AccuracyRate")]
        public double AccuracyRate
        {
            get
            {
                return m_accuracyRate;
            }
            set
            {
                m_accuracyRate = value;
                NotifyPropertyChanged("AccuracyRate");
            }
        }

        [XmlIgnore()]
        public Ability QAbility
        {
            get
            {
                return m_qAbility;
            }
            private set
            {
                m_qAbility = value;
                NotifyPropertyChanged("QAbility");
            }
        }

        [XmlAttribute("QAbilityKey")]
        public string QAbilityKey
        {
            get
            {
                return m_qAbilityKey;
            }
            set
            {
                m_qAbilityKey = value;
                NotifyPropertyChanged("QAbilityKey");
                //if (m_initialized)
                //{
                //    foreach (var ability in m_manager.CurrentCampaign.Abilities)
                //    {
                //        if (ability.Name.Equals(m_qAbilityKey))
                //        {
                //            QAbility = ability;
                //            break;
                //        }
                //    }
                //}
            }
        }

        public void PostDeserialize(ProjectManager sender)
        {
            m_manager = sender;
            m_initialized = true;
            //foreach (var ability in m_manager.CurrentCampaign.Abilities)
            //{
            //    if (ability.Name.Equals(m_qAbilityKey))
            //    {
            //        QAbility = ability;
            //        break;
            //    }
            //}
        }

        private ProjectManager m_manager;
        private bool m_initialized;
        private double m_baseHealth;
        private double m_healthRate;
        private double m_baseMana;
        private double m_manaRate;
        private double m_baseStrength;
        private double m_strengthRate;
        private double m_baseMagic;
        private double m_magicRate;
        private double m_baseSpeed;
        private double m_speedRate;
        private double m_baseAccuracy;
        private double m_accuracyRate;
        private string m_qAbilityKey;
        private Ability m_qAbility;
        private string m_wAbilityKey;
        private Ability m_wAbility;
        private string m_eAbilityKey;
        private Ability m_eAbility;
        private string m_passiveKey;
        private Ability m_passive;
    }
}
