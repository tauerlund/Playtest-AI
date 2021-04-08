using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace GeneticAlgo.GeneticAlgorithmGUI {
  public class FitnessGraph : Chart {

    private ChartArea graph;

    private Legend graphLegend;

    private Series bestAgent;
    private Series topAverage;
    private Series averageFitness;

    private Font font;

    public FitnessGraph(Size size) {

      font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

      CreateTitle();
      Size = size;

      BorderlineColor = SystemColors.ControlDark;
      BorderlineDashStyle = ChartDashStyle.Solid;

      graph = CreateChart();
      ChartAreas.Add(graph);

      graphLegend = CreateLegend();
      Legends.Add(graphLegend);

      averageFitness = CreateAvgFitness();
      bestAgent = CreateBestAgent();
      topAverage = CreateTopAverage();
      Series.Add(averageFitness);
      Series.Add(topAverage);
      Series.Add(bestAgent);
    }

    private void CreateTitle() {
      Title title = new Title();
      title.Font = font;
      title.Name = "GraphTitle";
      title.Text = "HISTORY";
      this.Titles.Add(title);
    }

    private ChartArea CreateChart() {
      ChartArea chartArea = new ChartArea();

      chartArea.AxisY.IsStartedFromZero = false;

      chartArea.AxisX.LineColor = Color.DimGray;
      chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
      chartArea.AxisX.MajorTickMark.Enabled = false;
      chartArea.AxisX.Title = "Generations";
      chartArea.AxisX.TitleFont = font;

      chartArea.AxisY.LineColor = Color.DimGray;
      chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
      chartArea.AxisY.MajorTickMark.Enabled = false;
      chartArea.AxisY.MajorTickMark.LineColor = Color.LightGray;
      chartArea.AxisY.Title = "Fitness";
      chartArea.AxisY.TitleFont = font;

      chartArea.Name = "Graph";

      return chartArea;
    }

    private Legend CreateLegend() {
      Legend legend = new Legend();

      legend.BorderColor = Color.LightGray;
      legend.Font = font;
      legend.LegendItemOrder = LegendItemOrder.SameAsSeriesOrder;
      legend.Name = "GraphLegend";
      legend.Title = "LEGEND";

      return legend;
    }

    private Series CreateTopAverage() {
      Series series = new Series();

      series.ChartArea = "Graph";

      series.BorderDashStyle = ChartDashStyle.Dash;
      series.BorderWidth = 2;
      series.Color = Color.RoyalBlue;

      series.ChartType = SeriesChartType.Spline;

      series.Legend = "GraphLegend";
      series.Name = "Selection average fitness";

      return series;
    }

    private Series CreateBestAgent() {
      Series series = new Series();

      series.ChartArea = "Graph";

      series.BorderDashStyle = ChartDashStyle.Solid;
      series.BorderWidth = 2;
      series.Color = Color.Red;

      series.ChartType = SeriesChartType.Line;

      series.Legend = "GraphLegend";
      series.Name = "Best agent fitness";

      return series;
    }

    private Series CreateAvgFitness() {
      Series series = new Series();

      series.ChartArea = "Graph";

      series.BorderDashStyle = ChartDashStyle.DashDotDot;
      series.BorderWidth = 2;
      series.Color = Color.Green;

      series.ChartType = SeriesChartType.Spline;

      series.Legend = "GraphLegend";
      series.Name = "Average fitness";

      return series;
    }

    public void UpdateGraph(int series, int generation, double fitness) {
      Series[series].Points.AddXY(generation, fitness);
    }
  }
}