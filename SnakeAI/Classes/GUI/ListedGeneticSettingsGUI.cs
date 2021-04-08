using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithmNS;

namespace SnakeAI {
  public class ListedGeneticSettingsGUI : ListedSettingsGUI {

    private GeneticSettings geneticSettings;

    private SettingItemGUI chromosomeSize;

    private NumericUpDown populationSizeControl;
    private NumericUpDown selectionSizeControl;
    private NumericUpDown mutationGeneControl;
    private NumericUpDown mutationAgentControl;
    private ComboBox selectionMethodControl;
    private ComboBox crossOverMethodControl;

    private CrossoverMethods crossoverMethods;
    private SelectionMethods selectionMethods;
    private SnakeAISettings snakeAISettings;

    public ListedGeneticSettingsGUI(SnakeAISettings snakeAISettings, bool canEdit) : base(canEdit) {
      this.snakeAISettings = snakeAISettings;
      geneticSettings = snakeAISettings.GeneticSettings;
      CreateItems();
      AddSettingItems();
    }

    public void CreateItems() {
      populationSizeControl = new NumericUpDown();
      populationSizeControl.Maximum = int.MaxValue;
      SettingItemGUI populationSize = new SettingItemGUI("Population size", 
                                                          geneticSettings.PopulationSize.ToString(),
                                                          populationSizeControl
                                                          );

      selectionSizeControl = new NumericUpDown();
      selectionSizeControl.Maximum = populationSizeControl.Value-1;
      SettingItemGUI selectionSize = new SettingItemGUI("Selection size", 
                                                         geneticSettings.SelectionSize.ToString(),
                                                         selectionSizeControl);

      populationSizeControl.ValueChanged += OnPopulationSizeChanged;

      mutationGeneControl = new NumericUpDown();
      mutationGeneControl.Maximum = 100;
      mutationGeneControl.DecimalPlaces = 1;
      mutationGeneControl.Increment = 0.1M;
      SettingItemGUI geneMutation = new SettingItemGUI("Gene mutation chance", 
                                                        (geneticSettings.MutationProbabilityGenes * 100).ToString(),
                                                        mutationGeneControl);

      mutationAgentControl = new NumericUpDown();
      mutationAgentControl.Maximum = 100;
      mutationAgentControl.DecimalPlaces = 1;
      mutationAgentControl.Increment = 0.1M;
      SettingItemGUI agentMutation = new SettingItemGUI("Agent mutation chance", 
                                                         (geneticSettings.MutationProbabiltyAgents * 100).ToString(),
                                                         mutationAgentControl);

      chromosomeSize = new SettingItemGUI("Chromesome size", 
                                                          geneticSettings.GeneCount.ToString(),
                                                          new TextBox());
      chromosomeSize.EditControl.Enabled = false;

      selectionMethods = new SelectionMethods();
      selectionMethodControl = new ComboBox();
      selectionMethodControl.DropDownStyle = ComboBoxStyle.DropDownList;
      selectionMethodControl.Items.Add(selectionMethods.TopPerformersSelector);
      selectionMethodControl.Items.Add(selectionMethods.RouletteWheelSelector);
      selectionMethodControl.SelectedItem = selectionMethodControl.Items[0];
      SettingItemGUI selectionMethod = new SettingItemGUI("Selection method", 
                                                           geneticSettings.Selector.ToString(),
                                                           selectionMethodControl);

      crossoverMethods = new CrossoverMethods();
      crossOverMethodControl = new ComboBox();
      crossOverMethodControl.DropDownStyle = ComboBoxStyle.DropDownList;
      crossOverMethodControl.Items.Add(crossoverMethods.OnePointCombineCrossoverRegular);
      crossOverMethodControl.Items.Add(crossoverMethods.OnePointCombinePlusElitismCrossover);
      crossOverMethodControl.SelectedItem = crossOverMethodControl.Items[0];

      SettingItemGUI crossOverMethod = new SettingItemGUI("Crossover method", 
                                                           geneticSettings.Crossover.ToString(),
                                                           crossOverMethodControl);

      settingItems.Add(populationSize);
      settingItems.Add(selectionSize);
      settingItems.Add(geneMutation);
      settingItems.Add(agentMutation);
      settingItems.Add(chromosomeSize);
      settingItems.Add(selectionMethod);
      settingItems.Add(crossOverMethod);
    }

    private void OnPopulationSizeChanged(object sender, EventArgs e) {
      selectionSizeControl.Maximum = populationSizeControl.Value - 1;
    }

    public void SaveSettings() {
      ResetFontType();
      snakeAISettings.GeneticSettings.UpdateGeneticSettings(snakeAISettings.NetworkSettings.numberOfWeights,
                                                            (int)populationSizeControl.Value,
                                                            (int)selectionSizeControl.Value,
                                                            (double)mutationAgentControl.Value / 100,
                                                            (double)mutationGeneControl.Value / 100,
                                                            (ISelector)selectionMethodControl.SelectedItem,
                                                            (ICrossover)crossOverMethodControl.SelectedItem);
      chromosomeSize.Value.Text = geneticSettings.GeneCount.ToString();
      chromosomeSize.EditControl.Text = geneticSettings.GeneCount.ToString();
    }
  }
}
