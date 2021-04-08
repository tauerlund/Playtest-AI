using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NeuralNetworkNS;

namespace Main.ProgramGUI {

  public class HiddenNeuronControlGUI : Control {

    private NetworkSettings networkSettings;
    public List<NumericUpDown> hiddenNeurons { get; set; }
    private TableLayoutPanel container;

    public HiddenNeuronControlGUI(NetworkSettings networkSettings) {
      this.networkSettings = networkSettings;

      container = new TableLayoutPanel();

      container.ColumnCount = 2;
      container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

      container.AutoSize = true;
      container.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      container.Dock = DockStyle.Fill;

      container.ControlAdded += OnControlAdded;
      container.ControlRemoved += OnControlDeleted;

      AutoSize = true;
      Dock = DockStyle.Fill;

      hiddenNeurons = new List<NumericUpDown>();

      for (int i = 0; i < networkSettings.numberOfHiddenNeurons.Length; i++) {
        AddLayer(i, networkSettings.numberOfHiddenNeurons[i]);
      }

      Controls.Add(container);
    }

    public void AddLayer(int index, int value) {
      //SuspendLayout();
      container.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
      container.RowCount += 1;

      hiddenNeurons.Add(new NumericUpDown());
      hiddenNeurons[index].Name = "neuron" + index;
      hiddenNeurons[index].Value = value;
      hiddenNeurons[index].Dock = DockStyle.Left;
      hiddenNeurons[index].Minimum = 1;

      container.Controls.Add(hiddenNeurons[index], 0, index);

      container.Controls.Add(new Label() { Text = $"in layer {(hiddenNeurons.Count)}",
                                           Anchor = AnchorStyles.Left,
                                           Name = "label" + index },
                                           1, index);

      //ResumeLayout();
    }

    public void OnControlAdded(object sender, ControlEventArgs e) {
      if (e.Control is NumericUpDown) {
        Height += ConstantsGUI.BOX_SIZE;
      }
    }

    public void OnControlDeleted(object sender, ControlEventArgs e) {
      if (e.Control is NumericUpDown) {
        Height -= ConstantsGUI.BOX_SIZE;
      }
    }

    public void RemoveLayer(int index) {
      //SuspendLayout();
      container.Controls.RemoveByKey("neuron" + index);
      container.Controls.RemoveByKey("label" + index);
      hiddenNeurons.RemoveAt(index);

      container.RowCount -= 1;

      //ResumeLayout();
    }
  }
}
