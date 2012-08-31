﻿using HubrisEditor.GameData;
using HubrisEditor.ProjectIO;
using HubrisEditor.Xaml.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HubrisEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            m_projectManager = new ProjectManager();
            DataContext = m_projectManager;
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (m_projectManager != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewProjectDialog dialog = new NewProjectDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                m_projectManager.NewProject(dialog.NameTextBox.Text, dialog.ScenarioTextBox.Text);
            }
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (m_projectManager != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml;";
            bool result = (bool)dialog.ShowDialog();
            if (result)
            {
                m_projectManager.OpenProject(dialog.FileName);
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (m_projectManager != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            m_projectManager.SaveProject();
        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (m_projectManager != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            bool result = (bool)dialog.ShowDialog();
            if (result)
            {
                m_projectManager.SaveProjectAs(dialog.FileName);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddScenarioMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            m_projectManager.CurrentCampaign.Scenarios.Add(new Scenario() { Name = "New Scenario " + m_random.Next(100000).ToString() });
        }

        private void AddTileTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            m_projectManager.CurrentCampaign.TileTypes.Add(new TileType() { Name = "New Tile Type " + m_random.Next(100000).ToString() });
        }

        private void TileTypeColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorSwatch swatch = new ColorSwatch();
            swatch.Owner = this;
            swatch.Closing += Swatch_Closing;
            swatch.DataContext = (sender as Button).DataContext;
            swatch.Show();
        }

        private void Swatch_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ColorSwatch swatch = sender as ColorSwatch;
            TileType context = swatch.DataContext as TileType;
            context.TileColor = new Color() { R = swatch.HSVColorSwatch.Red, G = swatch.HSVColorSwatch.Green, B = swatch.HSVColorSwatch.Blue, A = swatch.HSVColorSwatch.Alpha };
        }

        private Random m_random;
        private ProjectManager m_projectManager;
    }
}
