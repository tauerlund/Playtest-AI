using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGameNS {
  /// <summary>
  /// Controls all the graphical parts defined in class SnakeGameGUI.
  /// </summary>
  public partial class SnakeGameWindow : Form {
    // The declaretions of the variables used to render the game.
    private SnakeGameGUI snakeGameGUI;
    private int gridWidth;
    private int gridHeight;
    private int sideLength;
    private Bitmap Bitmap;
    private Graphics Graphics;
    private Button BtnReset;
    private Timer Timer;
    private Label LblScore;
    private Label LblEndScreen;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnakeGameWindow"/> class.
    /// </summary>
    public SnakeGameWindow(SnakeGameGUI snakeGameGUI) {
      this.snakeGameGUI = snakeGameGUI;
      gridWidth = snakeGameGUI.GridGUI.GridWidth;
      gridHeight = snakeGameGUI.GridGUI.GridHeight;
      sideLength = snakeGameGUI.GridGUI.SideLength;

      SetUpBitMap();
      SetUpLabels();
      SetUpButtons();
      AddControls();
      // Enables double buffering to smooth the rendering.
      EnableDoubleBuffering();
      // Timer to control the framerate.
      Timer = new Timer();
      Timer.Interval = 1; 

      SetUpEventArguments();
      SetUpWindowSettings();
      // Start the timer, which starts the game.
      Timer.Start();
    }

    // Settings for window.
    private void SetUpWindowSettings() {
      this.Width = Bitmap.Width + sideLength;
      this.Height = Bitmap.Height + sideLength * 2 + BtnReset.Height;
      this.Text = "Snake Game";
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.CenterToScreen();
    }

    /// <summary>
    /// Method to enable double buffering to smooth the rendering.
    /// </summary>
    private void EnableDoubleBuffering() {
      // Set the value of the double-buffering style bits to true.
      this.SetStyle(ControlStyles.DoubleBuffer |
         ControlStyles.UserPaint |
         ControlStyles.AllPaintingInWmPaint,
         true);
      this.UpdateStyles();
    }

    /// <summary>
    /// Method to update the label which prints out the score.
    /// </summary>
    private void UpdateLblScore() {
      LblScore.Text = "Score: " + snakeGameGUI.Score.ToString();
    }

    /// <summary>
    /// Method the to stop the game.
    /// </summary>
    private void StopGame() {
      LblEndScreen.Text = "Your total score is: " + snakeGameGUI.Score.ToString();
      Controls.Add(LblEndScreen);
      BtnReset.Enabled = true;
    }

    ////////////////////////////////////////////////
    ///              Event Arguments             ///
    ////////////////////////////////////////////////

    /// <summary>
    /// Event handler that draws the Grid onto the Bitmap, on paint event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Paint event argument.</param>
    
    private void OnPaint(Object sender, PaintEventArgs e) {
      Graphics.Clear(Color.Empty);
      snakeGameGUI.Draw(Graphics);
      e.Graphics.DrawImage(Bitmap, new System.Drawing.Point(0, 0));
    }

    /// <summary>
    /// Event handler that resets the game, on button click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Event argument.</param>
    private void OnClickReset(Object sender, EventArgs e) {
      Invalidate();
      BtnReset.Enabled = false;
      Controls.Remove(LblEndScreen);
      Timer.Start();
    }

    /// <summary>
    /// Event handler to set a steady framerate, and freezes the game if the snake dies.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TimerEvent(Object sender, EventArgs e) {
      if(!snakeGameGUI.IsAlive) {
        StopGame();
        Invalidate();
      }
      else {
        UpdateLblScore();
        Invalidate();
      }
    }

    /// <summary>
    /// Sets up the graphical area to draw the grid.
    /// </summary>
    private void SetUpBitMap() {
      Bitmap = new Bitmap(gridWidth, gridHeight);
      Graphics = Graphics.FromImage(Bitmap);
    }

    /// <summary>
    /// Sets up labels to print the score, and to print an end screen.
    /// </summary>
    private void SetUpLabels() {
      LblScore = new Label {
        Location = new System.Drawing.Point(Bitmap.Width - 75, Bitmap.Height),
        Text = "Score: " + snakeGameGUI.Score.ToString()
      };

      LblEndScreen = new Label();
      LblEndScreen.AutoSize = true;
      LblEndScreen.Location = new System.Drawing.Point(((gridWidth) / 2) - LblEndScreen.Width / 2, gridHeight / 4);
    }

    /// <summary>
    /// Sets up the buttons in the game.
    /// </summary>
    private void SetUpButtons() {
      // Button to reset the game.
      BtnReset = new Button {
        Location = new System.Drawing.Point(0, gridHeight),
        Text = "Restart",
        TabIndex = 0,
        Enabled = false
      };
    }

    /// <summary>
    /// Adds controls to collection.
    /// </summary>
    private void AddControls() {
      Controls.Add(BtnReset);
      Controls.Add(LblScore);
    }


    /// <summary>
    /// Sets up EventArguments for painting the grid, to reset the game and to control if the game has ended.
    /// </summary>
    private void SetUpEventArguments() {
      Paint += OnPaint;
      BtnReset.Click += OnClickReset;
      Timer.Tick += TimerEvent;
    }




  }
}
