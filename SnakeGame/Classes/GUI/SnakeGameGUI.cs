using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameNS {

  /// <summary>
  /// The class that contains game parts.
  /// </summary>
  [Serializable]
  public class SnakeGameGUI {

    public GridGUI GridGUI { get; private set; } 
    public bool IsAlive { get; private set; }
    public int Score { get; private set; }
    private Thread windowRenderingThread = null;
    private SnakeGameWindow snakeGameWindow;

    /// <summary>
    /// Property for the settings of the game.
    /// </summary>

    public bool WindowRunning;

    // If snake game provided
    public SnakeGameGUI(SnakeSettings snakeSettings, SnakeGame snakeGame) {
      IsAlive = snakeGame.Snake.IsAlive; 
      Score = snakeGame.Score; 
      GridGUI = new GridGUI(snakeGame.Grid, snakeSettings, IsAlive);
    }

    // If grid provided but no snake game
    public SnakeGameGUI(SnakeSettings snakeSettings, Grid grid, int score, bool isAlive) {
      IsAlive = isAlive;
      Score = score; 
      GridGUI = new GridGUI(grid, snakeSettings, IsAlive);
    }

    // Opens up a winform window with the snake game in its' own thread
    public void OpenGameWindow() {
      WindowRunning = true;
      windowRenderingThread = new Thread(WindowRendering);
      windowRenderingThread.IsBackground = true; // Will close with main thread.
      windowRenderingThread.Start();
    }

    private void WindowRendering() {
      snakeGameWindow = new SnakeGameWindow(this);
      snakeGameWindow.FormClosed += OnCloseSnakeWindow;
      Application.Run(snakeGameWindow);
    }

    private void OnCloseSnakeWindow(object sender, FormClosedEventArgs e) {
      WindowRunning = false;
    }
    /// <summary>
    /// Updates the view if snake game is provided.
    /// </summary>
    public void UpdateView(SnakeGame snakeGame) {
      IsAlive = snakeGame.Snake.IsAlive;
      Score = snakeGame.Score;
      GridGUI.Update(snakeGame.Grid, snakeGame.Snake.Head.Point, snakeGame.Snake.IsAlive); 
    }

    /// <summary>
    /// Updates the view if only grid is provided.
    /// </summary>
    public void UpdateView(Grid grid, int score, bool isAlive, Point snakeHeadPoint) {
      IsAlive = isAlive;
      Score = score;
      GridGUI.Update(grid, snakeHeadPoint, isAlive);
    }

    /// <summary>
    /// Draws each of the fields in the grid.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public void Draw(Graphics graphics) {
      GridGUI.Draw(graphics);
    }
  }
}
