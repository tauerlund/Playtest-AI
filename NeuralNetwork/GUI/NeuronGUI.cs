using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkNS {
  class NeuronGUI : Control {

    public Neuron LogicNeuron { get; private set; }
    
    public Label Value { get; private set; }

    public NeuronGUI(Neuron logicNeuron) {
      LogicNeuron = logicNeuron;

      Dock = DockStyle.Fill;

      Value = new Label();
      Value.Dock = DockStyle.Fill;
      if(logicNeuron is OutputNeuron) {
        Value.Text = ((OutputNeuron)logicNeuron).Label.ToString();
      }
      else {
        Value.Text = logicNeuron.Output.ToString();
      }

      Controls.Add(Value);
    }
  }
}
