using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeAI {
  class PlaytestResultsDetailsGUI : Form {

    private PlaytestResults playtestResults;

    private DataGridView dataGridView;

    private int speedMS;

    public PlaytestResultsDetailsGUI(PlaytestResults testResults) {

      this.playtestResults = testResults;
      Text = "Playtest all results";

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      Size = new Size(450, 600);

      AutoScroll = true;          

      InitializeComponent();
    }

    private void InitializeComponent() {
      dataGridView = CreateDataGridView();
      Controls.Add(dataGridView);
    }

    private DataGridView CreateDataGridView() {
      DataGridView dataGridView = new DataGridView();

      DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
      
      dataGridView.AutoSize = true;
      dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dataGridView.Dock = DockStyle.Fill;

      dataGridView.ColumnCount = 4;

      dataGridView.Columns[0].HeaderText = "ID";
      dataGridView.Columns[1].HeaderText = "Result";
      dataGridView.Columns[2].HeaderText = "Score";
      dataGridView.Columns[3].HeaderText = "Moves";
      dataGridView.Columns.Add(btnColumn);

      dataGridView.AllowUserToResizeColumns = false;
      dataGridView.AllowUserToResizeRows = false;

      dataGridView.AllowUserToOrderColumns = true;

      dataGridView.CellClick += OnCellClick;

      dataGridView.CellFormatting += OnCellFormat;

      for (int i = 0; i < dataGridView.ColumnCount; i++) {
        dataGridView.Columns[i].ReadOnly = true;
      }

      dataGridView.RowCount = playtestResults.TestResults.Count;

      for(int i = 0; i < playtestResults.TestResults.Count; i++) {
        dataGridView.Rows[i].Cells[0].Value = i;
        dataGridView.Rows[i].Cells[1].Value = playtestResults[i].TestResult;
        dataGridView.Rows[i].Cells[2].Value = playtestResults[i].PlaytestRecorder.EndTestInfo.Score;
        dataGridView.Rows[i].Cells[3].Value = playtestResults[i].PlaytestRecorder.EndTestInfo.MovesTotal;
        dataGridView.Rows[i].Cells[4].Value = "Replay";        
      }

      return dataGridView;
    }

    private void OnCellClick(object sender, DataGridViewCellEventArgs e) {
      if(e.RowIndex >= 0) {
        int index = (int)dataGridView.Rows[e.RowIndex].Cells[0].Value;

        if (e.ColumnIndex == 4) {
          PlaytestReplay replayer = new PlaytestReplay(playtestResults.TestResults[index].PlaytestRecorder);

          MilisecondsForm milisecondsForm = new MilisecondsForm();
          milisecondsForm.ShowDialog();
          speedMS = milisecondsForm.speedMS;

          replayer.ShowReplay(speedMS);
        }
      }
    }

    private void OnCellFormat(object sender, DataGridViewCellFormattingEventArgs e) {
      if(e.ColumnIndex == 1) {
        if(e.Value.Equals(TestResult.Failed)) {
          e.CellStyle.ForeColor = Color.Red;
        }
        else if(e.Value.Equals(TestResult.Passed)) {
          e.CellStyle.ForeColor = Color.Green;
        }
      }
    }

    private void OnColumnHeaderDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
      dataGridView.Sort(dataGridView.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Ascending);
    }
  }

  public class MilisecondsForm : Form {
    private NumericUpDown speedControl;
    public int speedMS;

    public MilisecondsForm() {
      Height = 120;
      Width = 250;

      StartPosition = FormStartPosition.CenterParent;

      FormBorderStyle = FormBorderStyle.None;
      MaximizeBox = false;
      MinimizeBox = false;

      TableLayoutPanel layout = new TableLayoutPanel();
      layout.Dock = DockStyle.Fill;
      layout.RowCount = 3;
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
      
      Label info = new Label();
      info.AutoSize = true;
      info.Text = "Please enter speed in ms:";
      info.Font = ConstantsGUI.FONT_SMALL;
      info.Anchor = AnchorStyles.Top;

      speedControl = new NumericUpDown();
      speedControl.Dock = DockStyle.Fill;
      speedControl.Minimum = 1;
      speedControl.Maximum = int.MaxValue;

      Button button = new Button();
      button.Text = "Confirm";
      button.Click += OnClick;
      button.Dock = DockStyle.Fill;
      button.Font = ConstantsGUI.FONT_SMALL;

      layout.Controls.Add(info, 0, 0);
      layout.Controls.Add(speedControl, 0, 1);
      layout.Controls.Add(button, 0, 2);
           
      Controls.Add(layout);
    }

    private void OnClick(object sender, EventArgs e) {
      speedMS = (int)speedControl.Value;
      Close();
    }
  }
}
