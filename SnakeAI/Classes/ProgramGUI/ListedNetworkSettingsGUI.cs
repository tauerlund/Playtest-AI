using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NeuralNetworkNS;

namespace Main.ProgramGUI {
  class ListedNetworkSettingsGUI : ListedSettingsGUI {
    private NetworkSettings networkSettings;

    private HiddenNeuronControlGUI hiddenNeuronsControl;

    private NumericUpDown hiddenLayersControl;

    private decimal hiddenLayersPrev;

    public ListedNetworkSettingsGUI(NetworkSettings networkSettings, bool canEdit) : base(canEdit) {
      this.networkSettings = networkSettings;
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
      hiddenLayersControl.Value = networkSettings.numberOfHiddenNeurons.Length;
      hiddenLayersControl.ValueChanged += OnHiddenLayersChanged;
      hiddenLayersControl.Minimum = 1;
      hiddenLayersControl.Maximum = 6;
      hiddenLayersControl.ReadOnly = true;
      hiddenLayersPrev = hiddenLayersControl.Value;
      SettingItemGUI numberOfHiddenLayers = new SettingItemGUI("Number of hidden layers",
                                                                networkSettings.numberOfHiddenNeurons.Length.ToString(),
                                                                hiddenLayersControl);

      hiddenNeuronsControl = new HiddenNeuronControlGUI(networkSettings);
      SettingItemGUI numberOfHiddenNeurons = new SettingItemGUI("Number of hidden neurons",
                                                                networkSettings.numberOfHiddenNeurons.Length.ToString(),
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
      NumericUpDown num = (NumericUpDown)sender;
      if (hiddenLayersPrev > num.Value) {
        hiddenNeuronsControl.RemoveLayer(hiddenNeuronsControl.hiddenNeurons.Count-1);
        hiddenLayersPrev = num.Value;
      }
      else {
        hiddenNeuronsControl.AddLayer(hiddenNeuronsControl.hiddenNeurons.Count, 1);
        hiddenLayersPrev = num.Value;
        Parent.Height -= ConstantsGUI.BOX_SIZE; 
      }
    }

    public void SaveSettings() {
      networkSettings.numberOfHiddenNeurons = new int[(int)hiddenLayersControl.Value];

      for(int i = 0; i < (int)hiddenLayersControl.Value; i++) {
        networkSettings.numberOfHiddenNeurons[i] = (int)hiddenNeuronsControl.hiddenNeurons[i].Value;
      }
    }
  }
}
