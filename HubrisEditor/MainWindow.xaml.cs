﻿using HubrisEditor.Core;
using HubrisEditor.GameData;
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
            Scenario scenario = new Scenario() { Name = "New Scenario " + m_random.Next(100000).ToString() };
            scenario.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.Scenarios.Add(scenario);
        }

        private void AddTileTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileType type = new TileType() { Name = "New Tile Type " + m_random.Next(100000).ToString() };
            type.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileTypes.Add(type);
        }

        private void AddUnitPlacementMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileUnitPlacement unitPlacement = new TileUnitPlacement() { Name = "New Unit Placement " + m_random.Next(100000).ToString() };
            unitPlacement.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileUnitPlacements.Add(unitPlacement);
        }

        private void AddTileContentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileContent content = new TileContent() { Name = "New Tile Content " + m_random.Next(100000).ToString() };
            content.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileContents.Add(content);
        }


        private void TileTypeColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorSwatch swatch = new ColorSwatch();
            swatch.Owner = this;
            swatch.Closing += Swatch_Closing;
            TileType type = (sender as Button).DataContext as TileType;
            swatch.DataContext = type;
            if (type.TileColor != null)
            {
                swatch.HSVColorSwatch.Red = type.TileColor.R;
                swatch.HSVColorSwatch.Green = type.TileColor.G;
                swatch.HSVColorSwatch.Blue = type.TileColor.B;
                swatch.HSVColorSwatch.Alpha = type.TileColor.A;
            }
            swatch.Show();
        }

        private void UnitPlacementColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorSwatch swatch = new ColorSwatch();
            swatch.Owner = this;
            swatch.Closing += PlacementSwatch_Closing;
            TileUnitPlacement unitPlacement = (sender as Button).DataContext as TileUnitPlacement;
            swatch.DataContext = unitPlacement;
            if (unitPlacement.TileColor != null)
            {
                swatch.HSVColorSwatch.Red = unitPlacement.TileColor.R;
                swatch.HSVColorSwatch.Green = unitPlacement.TileColor.G;
                swatch.HSVColorSwatch.Blue = unitPlacement.TileColor.B;
                swatch.HSVColorSwatch.Alpha = unitPlacement.TileColor.A;
            }
            swatch.Show();
        }

        private void TileContentColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorSwatch swatch = new ColorSwatch();
            swatch.Owner = this;
            swatch.Closing += ContentSwatch_Closing;
            TileContent content = (sender as Button).DataContext as TileContent;
            swatch.DataContext = content;
            if (content.TileColor != null)
            {
                swatch.HSVColorSwatch.Red = content.TileColor.R;
                swatch.HSVColorSwatch.Green = content.TileColor.G;
                swatch.HSVColorSwatch.Blue = content.TileColor.B;
                swatch.HSVColorSwatch.Alpha = content.TileColor.A;
            }
            swatch.Show();
        }

        private void Swatch_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ColorSwatch swatch = sender as ColorSwatch;
            TileType context = swatch.DataContext as TileType;
            context.TileColor = new Color() { R = swatch.HSVColorSwatch.Red, G = swatch.HSVColorSwatch.Green, B = swatch.HSVColorSwatch.Blue, A = swatch.HSVColorSwatch.Alpha };
        }

        private void PlacementSwatch_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ColorSwatch swatch = sender as ColorSwatch;
            TileUnitPlacement context = swatch.DataContext as TileUnitPlacement;
            context.TileColor = new Color() { R = swatch.HSVColorSwatch.Red, G = swatch.HSVColorSwatch.Green, B = swatch.HSVColorSwatch.Blue, A = swatch.HSVColorSwatch.Alpha };
        }

        private void ContentSwatch_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ColorSwatch swatch = sender as ColorSwatch;
            TileContent context = swatch.DataContext as TileContent;
            context.TileColor = new Color() { R = swatch.HSVColorSwatch.Red, G = swatch.HSVColorSwatch.Green, B = swatch.HSVColorSwatch.Blue, A = swatch.HSVColorSwatch.Alpha };
        }

        private void GenerateTilesButton_Click(object sender, RoutedEventArgs e)
        {
            ((sender as Button).DataContext as Scenario).GenerateTiles();
        }

        private void ReplaceTilesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ScenariosListBox.SelectedItem != null)
            {
                Scenario scenario = ScenariosListBox.SelectedItem as Scenario;
                ReplaceTilesWindow window = new ReplaceTilesWindow(m_projectManager.CurrentCampaign);
                window.ShowDialog();
                if (window.ReplaceComboBox.SelectedItem != null && window.WithComboBox.SelectedItem != null)
                {
                    TileType replace = window.ReplaceComboBox.SelectedItem as TileType;
                    TileType with = window.WithComboBox.SelectedItem as TileType;
                    foreach (var tileSlot in scenario.TileSlots)
                    {
                        if (tileSlot.TileTypeKey == replace.Name)
                        {
                            tileSlot.TileTypeKey = with.Name;
                        }
                    }
                }
            }
        }

        private void UpScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScenariosListBox.SelectedItem != null)
            {
                if (ScenariosListBox.SelectedIndex == 0)
                {
                    return;
                }
                Scenario scenario = ScenariosListBox.SelectedItem as Scenario;
                int index = ScenariosListBox.SelectedIndex - 1;
                m_projectManager.CurrentCampaign.Scenarios.Move(ScenariosListBox.SelectedIndex, index);
            }
        }

        private void DownScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScenariosListBox.SelectedItem != null)
            {
                if (ScenariosListBox.SelectedIndex == m_projectManager.CurrentCampaign.Scenarios.Count - 1)
                {
                    return;
                }
                Scenario scenario = ScenariosListBox.SelectedItem as Scenario;
                int index = ScenariosListBox.SelectedIndex + 1;
                m_projectManager.CurrentCampaign.Scenarios.Move(ScenariosListBox.SelectedIndex, index);
            }
        }

        private void DeleteScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScenariosListBox.SelectedItem != null)
            {
                Scenario scenario = ScenariosListBox.SelectedItem as Scenario;
                m_projectManager.CurrentCampaign.Scenarios.Remove(scenario);
                ScenariosListBox.SelectedIndex = 0;
            }
        }

        private void AddScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            Scenario scenario = new Scenario() { Name = "New Scenario " + m_random.Next(100000).ToString() };
            scenario.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.Scenarios.Add(scenario);
        }

        private void GenerateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            GenerateWindow window = new GenerateWindow(m_projectManager.CurrentCampaign);
            window.ShowDialog();
            if (!window.DialogResult == true)
            {
                return;
            }

            int value = (int)window.MainSlider.Value;
            bool success = (window.FirstComboBox.SelectedItem != null) && (window.SecondComboBox.SelectedItem != null);

            if (!success)
            {
                return;
            }

            int random = m_random.Next();
            PerlinNoise noise = new PerlinNoise(random);
            byte[] pixels = noise.GetPixels(128, 128);

            Scenario scenario = ScenariosListBox.SelectedItem as Scenario;
            double ratiox = 128.0 / scenario.CanvasSpaceWidth;
            double ratioy = 128.0 / scenario.CanvasSpaceHeight;

            for(int i = 0; i < scenario.CanvasSpaceHeight; i++)
            {
                for (int j = 0; j < scenario.CanvasSpaceWidth; j++)
                {
                    TileSlot tile = scenario.TileSlots[i * scenario.CanvasSpaceWidth + j];
                    int point = pixels[((int)(i * ratioy)) * 128 + ((int)(j * ratiox))];
                    if (point <= value)
                    {
                        tile.TileTypeKey = (window.FirstComboBox.SelectedItem as TileType).Name;
                    }
                    else
                    {
                        tile.TileTypeKey = (window.SecondComboBox.SelectedItem as TileType).Name;
                    }
                }
            }
        }

        private void AddTileTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileType type = new TileType() { Name = "New Tile Type " + m_random.Next(100000).ToString() };
            type.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileTypes.Add(type);
        }

        private void DeleteTileTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (TileTypesListBox.SelectedItem != null)
            {
                TileType type = TileTypesListBox.SelectedItem as TileType;
                m_projectManager.CurrentCampaign.TileTypes.Remove(type);
                TileTypesListBox.SelectedIndex = 0;
            }
        }

        private void AddUnitPlacementButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileUnitPlacement unitPlacement = new TileUnitPlacement() { Name = "New Unit Placement " + m_random.Next(100000).ToString() };
            unitPlacement.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileUnitPlacements.Add(unitPlacement);
        }

        private void DeleteUnitPlacementButton_Click(object sender, RoutedEventArgs e)
        {
            if (UnitPlacementListBox.SelectedItem != null)
            {
                TileUnitPlacement tup = UnitPlacementListBox.SelectedItem as TileUnitPlacement;
                m_projectManager.CurrentCampaign.TileUnitPlacements.Remove(tup);
                UnitPlacementListBox.SelectedIndex = 0;
            }
        }

        private void AddTileContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_random == null)
            {
                m_random = new Random();
            }
            TileContent content = new TileContent() { Name = "New Tile Content " + m_random.Next(100000).ToString() };
            content.PostDeserialize(m_projectManager);
            m_projectManager.CurrentCampaign.TileContents.Add(content);
        }

        private void DeleteTileContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (TileContentListBox.SelectedItem != null)
            {
                TileContent content = TileContentListBox.SelectedItem as TileContent;
                m_projectManager.CurrentCampaign.TileContents.Remove(content);
                TileContentListBox.SelectedIndex = 0;
            }
        }

        private void PerlinMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PerlinTestWindow window = new PerlinTestWindow();
            window.Show();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.FocusedElement as TextBox == null && e.Key == Key.Space)
            {
                SlotGrid.HandleSpaceDown();
                e.Handled = true;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.FocusedElement as TextBox == null && e.Key == Key.Space)
            {
                SlotGrid.HandleSpaceUp();
                e.Handled = true;
            }
        }

        private Random m_random;
        private ProjectManager m_projectManager;
    }
}
