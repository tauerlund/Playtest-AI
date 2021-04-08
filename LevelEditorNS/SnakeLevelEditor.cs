using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGameNS;
using System.Drawing;
using System.Timers;
using System.IO;
using HelperMethods;

namespace LevelEditorNS {
  public class SnakeLevelEditor : Form {
    private SnakeSettingsGUI snakeSettings;
    private FieldGUI[,] fields;

    private SnakeLevel level;

    private Graphics graphics;
    private Bitmap bitmap;

    private int clickedRow;
    private int clickedColumn;
    private System.Timers.Timer mouseTimer;
    private MouseButtons mouseButton;

    private StorageManager storageManager;

    private MenuStrip fileMenu;

    public SnakeLevelEditor(SnakeSettingsGUI snakeSettings) {
      this.snakeSettings = snakeSettings;
      fields = new FieldGUI[snakeSettings.RowCount, snakeSettings.ColumnCount];

      StartPosition = FormStartPosition.CenterScreen;

      storageManager = new StorageManager();

      CreateMenu();

      Text = "Snake Level Editor";

      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;

      mouseTimer = new System.Timers.Timer();
      mouseTimer.Interval = 1;
      mouseTimer.Enabled = false;

      // Graphical area to draw the grid.
      bitmap = new Bitmap(snakeSettings.ColumnCount * snakeSettings.SideLength, snakeSettings.RowCount * snakeSettings.SideLength);
      graphics = Graphics.FromImage(bitmap);

      Width = bitmap.Width + snakeSettings.SideLength;
      Height = bitmap.Height + (snakeSettings.SideLength * 2) + fileMenu.Height - 1;

      EnableDoubleBuffering();

      InitializeLevel();

      Paint += OnPaint;
      MouseMove += OnMouseMove;
      MouseDown += OnMouseDown;
      MouseUp += OnMouseUp;
      mouseTimer.Elapsed += mouseTimerEvent;

      fileMenu.ItemClicked += FileMenuOnClick;
    }

    private void EnableDoubleBuffering() {
      // Set the value of the double-buffering style bits to true.
      this.SetStyle(ControlStyles.DoubleBuffer |
         ControlStyles.UserPaint |
         ControlStyles.AllPaintingInWmPaint,
         true);
      this.UpdateStyles();
    }

    private void InitializeLevel() {
      for (int i = 0; i < snakeSettings.RowCount; i++) {
        for (int j = 0; j < snakeSettings.ColumnCount; j++) {
          if ((i == 0 || j == 0) || (i == snakeSettings.RowCount - 1 || j == snakeSettings.ColumnCount - 1)) {
            fields[i, j] = new WallGUI(new SnakeGameNS.Point(i, j), snakeSettings.SideLength);
          }
          else {
            fields[i, j] = new EmptyGUI(new SnakeGameNS.Point(i, j), snakeSettings.SideLength);
          }
        }
      }
      Invalidate();
    }

    private void OnPaint(Object sender, PaintEventArgs e) {
      graphics.Clear(Color.Empty);
      Draw(graphics);
      e.Graphics.DrawImage(bitmap, new System.Drawing.Point(0, fileMenu.Height));
    }

    public void Draw(Graphics graphics) {
      foreach (FieldGUI field in fields) {
        field.Draw(graphics);
      }
    }

    private void OnMouseDown(object sender, MouseEventArgs e) {
      mouseTimer.Enabled = true;
      mouseButton = e.Button;
    }

    private void OnMouseUp(object sender, MouseEventArgs e) {
      mouseTimer.Enabled = false;
    }

    private void OnMouseMove(object sender, MouseEventArgs e) {

      int mousePosY = (e.Y - fileMenu.Height) / snakeSettings.SideLength;
      int mousePosX = (e.X) / snakeSettings.SideLength;

      clickedRow = Math.Min(Math.Max(0, mousePosY), snakeSettings.RowCount - 1);
      clickedColumn = Math.Min(Math.Max(0, mousePosX), snakeSettings.ColumnCount - 1);
    }

    private void mouseTimerEvent(Object source, ElapsedEventArgs e) {
      if (mouseButton == MouseButtons.Left) {
        fields[clickedRow, clickedColumn] = new WallGUI(new SnakeGameNS.Point(clickedRow, clickedColumn), snakeSettings.SideLength);

        //BrushEffect(clickedRow, clickedColumn);
      }
      else if (mouseButton == MouseButtons.Right) {
        fields[clickedRow, clickedColumn] = new EmptyGUI(new SnakeGameNS.Point(clickedRow, clickedColumn), snakeSettings.SideLength);
      }
      Invalidate();
    }
    
    private void CreateMenu() {
      fileMenu = new MenuStrip();

      fileMenu.Items.Add("New level");
      fileMenu.Items.Add("Save level");
      fileMenu.Items.Add("Load level");

      MainMenuStrip = fileMenu;
      Controls.Add(fileMenu);
    }

    private void FileMenuOnClick(object sender, ToolStripItemClickedEventArgs e) {
      string menuText = e.ClickedItem.Text;

      switch (menuText) {
        case "New level":
          NewLevelClicked();
          break;
        case "Save level":
          SaveLevelClicked();
          break;
        case "Load level":
          LoadLevelClicked();
          break;
        default:
          break;
      }
    }

    private void NewLevelClicked() {

      if (MessageBox.Show("Are you sure you want to create a new level? Current level will NOT be saved.",
                          "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
        InitializeLevel();
      }
    }

    public static SnakeSettings ConvertSettingsGUIToLogic(SnakeSettingsGUI snakeSettings) {
      SnakeSettings snakeSettingsLogic = new SnakeSettings(snakeSettings.RowCount,
                                                     snakeSettings.ColumnCount,
                                                     snakeSettings.SideLength,
                                                     1,
                                                     new RandomNumberGenerator());

      return snakeSettingsLogic;
    }

    public static SnakeSettingsGUI ConvertSettingsLogicToGUI(SnakeSettings snakeSettings) {
      SnakeSettingsGUI snakeSettingsGUI = new SnakeSettingsGUI(snakeSettings.RowCount,
                                                               snakeSettings.ColumnCount,
                                                               snakeSettings.SideLengthGUI);

      return snakeSettingsGUI;
    }

    private void SaveLevelClicked() {
      SaveFileDialog saveFileDialog = new SaveFileDialog();

      SnakeSettings snakeSettingsLogic = ConvertSettingsGUIToLogic(snakeSettings);

      Field[,] logicFields = new Field[snakeSettings.RowCount,snakeSettings.ColumnCount];
      for(int i  = 0; i < snakeSettings.RowCount; i++) {
        for (int j = 0; j < snakeSettings.ColumnCount; j++) {
          if(fields[i,j] is WallGUI) {
            logicFields[i,j] = new Wall(fields[i,j].Point);
          }
          else if(fields[i,j] is EmptyGUI) {
            logicFields[i,j] = new Empty(fields[i,j].Point);
          }
        }
      }
      Grid savedGrid = new Grid(logicFields);

      Grid grid = new Grid(snakeSettings.RowCount, snakeSettings.ColumnCount);
      SnakeLevel savedLevel = new SnakeLevel(savedGrid, snakeSettingsLogic);
      
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\SavedLevels\"));
      saveFileDialog.Title = "Save Snake level";
      saveFileDialog.ShowHelp = true;

      if (saveFileDialog.ShowDialog() == DialogResult.OK) {
        try {
          storageManager.SaveObject(savedLevel, saveFileDialog.FileName);
        }
        catch (IOException) {
          MessageBox.Show("ERROR! Could not save. Is file in use by another process?");
        }
      }
    }

    private void LoadLevelClicked() {

      OpenFileDialog openFileDialog = new OpenFileDialog();

      openFileDialog.RestoreDirectory = true;
      openFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\SavedLevels\"));
      openFileDialog.Title = "Load Snake level";

      SnakeLevel loadedLevel;

      if (openFileDialog.ShowDialog() == DialogResult.OK) {
        loadedLevel = storageManager.LoadObject<SnakeLevel>(openFileDialog.FileName);

        GridGUI gridGUI = new GridGUI(loadedLevel.Grid, loadedLevel.SnakeSettings, false);

        fields = gridGUI.FieldGUIs;
        Invalidate();
      }
    }
  }
}
