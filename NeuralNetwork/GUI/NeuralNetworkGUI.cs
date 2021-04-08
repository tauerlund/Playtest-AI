using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NeuralNetworkNS {
  class NeuralNetworkGUI : Form {

    private TableLayoutPanel neuralNetworkLayout;

    private InputLayer inputLayer;
    private List<HiddenLayer> hiddenLayers;
    private OutputLayer outputLayer;
    private List<Layer> layers;

    private int neuronWidth;
    private int neuronHeight;

    public NeuralNetworkGUI(InputLayer inputLayer, List<HiddenLayer> hiddenLayers, OutputLayer outputLayer, List<Layer> layers) {
      this.inputLayer = inputLayer;
      this.hiddenLayers = hiddenLayers;
      this.outputLayer = outputLayer;
      this.layers = layers;

      neuronWidth = 40;
      neuronHeight = 40;

      Layer largestLayer = layers.OrderByDescending(n => n.Neurons.Count()).First();

      int layoutColumns = layers.Count();
      int layoutRows = largestLayer.Neurons.Count();

      Width = neuronWidth * layoutColumns;
      Height = neuronHeight * layoutRows;

      Padding padding = new Padding();
      padding.All = 10;
      this.Padding = padding;

      neuralNetworkLayout = CreateTable(layoutRows, layoutColumns);
      CreateNeurons();
      Controls.Add(neuralNetworkLayout);

      NeuronGUI neuronGUI = new NeuronGUI(inputLayer.Neurons[0]);
    }

    private TableLayoutPanel CreateTable(int rows, int columns) {
      TableLayoutPanel layout = new TableLayoutPanel();

      layout.Dock = DockStyle.Fill;

      layout.RowCount = rows;
      for(int i = 0; i < rows; i++) {
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rows));
      }

      layout.ColumnCount = columns;
      for (int i = 0; i < columns; i++) {
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / rows));
      }

      layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

      return layout;
    }

    private void CreateNeurons() {
      for(int i = 0; i < layers.Count(); i++) {
        for(int j = 0; j < layers[i].Neurons.Count(); j++) {
          NeuronGUI neuronGUI = new NeuronGUI(layers[i].Neurons[j]);          
          neuralNetworkLayout.Controls.Add(neuronGUI, i, j);
        }
      }
    }
  }
}
