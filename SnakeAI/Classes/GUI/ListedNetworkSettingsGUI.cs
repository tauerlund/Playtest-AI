using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NeuralNetworkNS;

namespace SnakeAI {
  class ListedNetworkSettingsGUI : ListedSettingsGUI {
    private NetworkSettings networkSettings;
    private SnakeAISettings snakeAISettings;

    private HiddenNeuronControlGUI hiddenNeuronsControl;

    private NumericUpDown hiddenLayersControl;

    private decimal hiddenLayersPrev;

    public ListedNetworkSettingsGUI(SnakeAISettings snakeAISettings, bool canEdit) : base(canEdit) {
      this.snakeAISettings = snakeAISettings;
      networkSettings = snakeAISettings.NetworkSettings;
      CreateItems();
      AddSettingItems();
    }

    private void CreateItems() {

      NumericUpDown inputNeuronsControl = new NumericUpDown();
      inputNeuronsControl.Minimum = 1;
      inputNeuronsControl.Value = networkSettings.numberOfInputNeurons;
      inputNeuronsControl.Enabled = false;
      SettingItemGUI numberOfInputNeurons = new SettingItemGUI("Number of input neurons",
                                                                networkSettings.numberOfInputNeurons.ToString(),
                                                                inputNeuronsControl);

      hiddenLayersControl = new NumericUpDown();
      hiddenLayersControl.Value = networkSettings.hiddenLayerStructure.Length;
      hiddenLayersControl.ValueChanged += OnHiddenLayersChanged;
      hiddenLayersControl.Minimum = 1;
      hiddenLayersControl.Maximum = 6;
      hiddenLayersControl.ReadOnly = true;
      hiddenLayersPrev = hiddenLayersControl.Value;
      SettingItemGUI numberOfHiddenLayers = new SettingItemGUI("Number of hidden layers",
                                                                networkSettings.hiddenLayerStructure.Length.ToString(),
                                                                hiddenLayersControl);

      hiddenNeuronsControl = new HiddenNeuronControlGUI(networkSettings);
      SettingItemGUI numberOfHiddenNeurons = new SettingItemGUI("Number of hidden neurons",
                                                                networkSettings.hiddenLayerStructure.Length.ToString(),
                                                                hiddenNeuronsControl);

      NumericUpDown outputNeuronsControl = new NumericUpDown();
      outputNeuronsControl.Minimum = 1;
      outputNeuronsControl.Value = networkSettings.numberOfOutputNeurons;
      outputNeuronsControl.Enabled = false;
      SettingItemGUI numberOfOutputNeurons = new SettingItemGUI("Number of output neurons",
                                                                networkSettings.numberOfOutputNeurons.ToString(),
                                                                outputNeuronsControl);
      settingItems.Add(numberOfInputNeurons);
      settingItems.Add(numberOfHiddenLayers);
      settingItems.Add(numberOfHiddenNeurons);
      settingItems.Add(numberOfOutputNeurons);
    }

    private void OnHiddenLayersChanged(object sender, EventArgs e) {
      SuspendLayout();
      NumericUpDown num = (NumericUpDown)sender;
      if (hiddenLayersPrev > num.Value) {
        hiddenNeuronsControl.RemoveLayer(hiddenNeuronsControl.hiddenNeurons.Count - 1);
        hiddenLayersPrev = num.Value;
      }
      else {
        hiddenNeuronsControl.AddLayer(hiddenNeuronsControl.hiddenNeurons.Count, 1);
        hiddenLayersPrev = num.Value;
        Parent.Height -= ConstantsGUI.BOX_SIZE;
      }
      ResumeLayout();
    }

    public void SaveSettings() {
      int[] newHiddenLayerStructure = new int[(int)hiddenLayersControl.Value];

      for (int i = 0; i < (int)hiddenLayersControl.Value; i++) {
        newHiddenLayerStructure[i] = (int)hiddenNeuronsControl.hiddenNeurons[i].Value;
      }

      snakeAISettings.NetworkSettings.UpdateNetworkSettings(newHiddenLayerStructure);
    }    
  }
}
