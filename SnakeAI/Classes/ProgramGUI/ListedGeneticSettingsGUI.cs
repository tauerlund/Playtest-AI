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

namespace Main.ProgramGUI {
  public class ListedGeneticSettingsGUI : ListedSettingsGUI {

    private GeneticSettings geneticSettings;

    private SettingItemGUI chromosomeSize;

    private NumericUpDown populationSizeControl;
    private NumericUpDown selectionSizeControl;
    private NumericUpDown mutationGeneControl;
    private NumericUpDown mutationAgentControl;
    private ComboBox selectionMethodControl;
    private ComboBox crossOverMethodControl;

    public ListedGeneticSettingsGUI(GeneticSettings geneticSettings, bool canEdit) : base(canEdit) {
      this.geneticSettings = geneticSettings;
      CreateItems();
      AddSettingItems();
    }

    public void CreateItems() {
      // FLYT TOOLTIPS STRINGS IND I GENETIC SETTINGS SÅ DE KAN GENBRUGES


      populationSizeControl = new NumericUpDown();
      populationSizeControl.Maximum = int.MaxValue;
      SettingItemGUI populationSize = new SettingItemGUI("Population size", 
                                                          geneticSettings.PopulationSize.ToString(),
                                                          populationSizeControl,
                                                          "test");
      //populationSize.ToolTipText = "The size of the population in each generation.";

      selectionSizeControl = new NumericUpDown();
      selectionSizeControl.Maximum = populationSizeControl.Value-1;
      SettingItemGUI selectionSize = new SettingItemGUI("Selection size", 
                                                         geneticSettings.SelectionSize.ToString(),
                                                         selectionSizeControl);
      //selectionSize.ToolTipText = "The number of agents chosen to be parents.";

      mutationGeneControl = new NumericUpDown();
      mutationGeneControl.Maximum = 100;
      mutationGeneControl.DecimalPlaces = 1;
      mutationGeneControl.Increment = 0.1M;
      SettingItemGUI geneMutation = new SettingItemGUI("Gene mutation chance", 
                                                        (geneticSettings.MutationProbabilityGenes * 100).ToString(),
                                                        mutationGeneControl);
      //geneMutation.ToolTipText = "The chance for any single gene to be mutated.";

      mutationAgentControl = new NumericUpDown();
      mutationAgentControl.Maximum = 100;
      mutationAgentControl.DecimalPlaces = 1;
      mutationAgentControl.Increment = 0.1M;
      SettingItemGUI agentMutation = new SettingItemGUI("Agent mutation chance", 
                                                         (geneticSettings.MutationProbabiltyAgents * 100).ToString(),
                                                         mutationAgentControl);
      //agentMutation.ToolTipText = "The chance for any single agent to be mutated.";

      chromosomeSize = new SettingItemGUI("Chromesome size", 
                                                          geneticSettings.GeneCount.ToString(),
                                                          new TextBox());
      //chromosomeSize.ToolTipText = "The number of genes in an agent's chromosome.";
      chromosomeSize.EditControl.Enabled = false;

      selectionMethodControl = new ComboBox();
      selectionMethodControl.DropDownStyle = ComboBoxStyle.DropDownList;
      selectionMethodControl.Items.Add(geneticSettings.SelectionMethod);
      SettingItemGUI selectionMethod = new SettingItemGUI("Selection method", 
                                                           geneticSettings.SelectionMethod.ToString(),
                                                           selectionMethodControl);
      //selectionMethod.ToolTipText = "The chosen method for selection."; // BESKRIV SPECIFIK METHOD

      crossOverMethodControl = new ComboBox();
      crossOverMethodControl.DropDownStyle = ComboBoxStyle.DropDownList;
      crossOverMethodControl.Items.Add(geneticSettings.CrossoverMethod);

      SettingItemGUI crossOverMethod = new SettingItemGUI("Crossover method", 
                                                           geneticSettings.CrossoverMethod.ToString(),
                                                           crossOverMethodControl);
      //crossOverMethod.ToolTipText = "The chosen method for crossover."; // BESKRIV SPECIFIK METHOD

      settingItems.Add(populationSize);
      settingItems.Add(selectionSize);
      settingItems.Add(geneMutation);
      settingItems.Add(agentMutation);
      settingItems.Add(chromosomeSize);
      settingItems.Add(selectionMethod);
      settingItems.Add(crossOverMethod);
    }

    public void SaveSettings() {
      chromosomeSize.Value.Text = geneticSettings.GeneCount.ToString();
      chromosomeSize.EditControl.Text = geneticSettings.GeneCount.ToString();

      geneticSettings.PopulationSize = (int)populationSizeControl.Value;
      geneticSettings.SelectionSize = (int)selectionSizeControl.Value;
      geneticSettings.MutationProbabilityGenes = (double)mutationGeneControl.Value / 100;
      geneticSettings.MutationProbabiltyAgents = (double)mutationAgentControl.Value / 100;
      geneticSettings.SelectionMethod = (SelectionMethod)selectionMethodControl.SelectedIndex;
      geneticSettings.CrossoverMethod = (CrossoverMethod)crossOverMethodControl.SelectedIndex;
    }
  }
}
