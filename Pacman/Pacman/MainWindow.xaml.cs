using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pacman
{
  public partial class MainWindow : Window
  {
    public Game game;
    private DispatcherTimer dispatcherTimer;

    public MainWindow()
    {
      InitializeComponent();
      game = new Game();

      dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
      dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
      dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
      dispatcherTimer.Start();

      this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
    }

    void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
      game.KeyPressed(e);
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
      game.PlayRound();
      if (!game.gameActive)
        dispatcherTimer.Stop();

      Paint();
      CommandManager.InvalidateRequerySuggested();
    }

    public void Paint()
    {
      canvas.Children.Clear();

      var map = game.map;
      int BLOCK_WIDTH = 15;

      Rectangle background = new Rectangle();
      SolidColorBrush mySolidColorBrush1 
        = new SolidColorBrush();
      mySolidColorBrush1.Color = Color.FromRgb(0, 0, 0);
      background.Fill = mySolidColorBrush1;
      background.StrokeThickness = 2;
      background.Stroke = Brushes.Black;
      background.Width = BLOCK_WIDTH * 28;
      background.Height = BLOCK_WIDTH * 32;
      Canvas.SetLeft(background, 0);
      Canvas.SetTop(background, 0);
      canvas.Children.Add(background);

      for (int i = 0; i < 32; i++)
        for (int j = 0; j < 28; j++)
        {
          if (map[i, j].entity != null)
          {
            if (map[i, j].entity?.GetType()
              .ToString().IndexOf("PacMan") >= 0)
            {
              Rectangle r = new System.Windows.Shapes.Rectangle();
              SolidColorBrush mySolidColorBrush 
                = new SolidColorBrush();
              mySolidColorBrush.Color 
                = Color.FromRgb(255, 255, 0);
              r.Fill = mySolidColorBrush;
              r.StrokeThickness = 1;
              r.Stroke = Brushes.Black;
              r.Width = BLOCK_WIDTH;
              r.Height = BLOCK_WIDTH;
              Canvas.SetLeft(r, j * BLOCK_WIDTH);
              Canvas.SetTop(r, i * BLOCK_WIDTH);
              canvas.Children.Add(r);
            }
            else
            {
              Rectangle r = new Rectangle();
              SolidColorBrush mySolidColorBrush 
                = new SolidColorBrush();
              mySolidColorBrush.Color 
                = Color.FromRgb(204, 0, 204);
              r.Fill = mySolidColorBrush;
              r.StrokeThickness = 1;
              r.Stroke = Brushes.Black;
              r.Width = BLOCK_WIDTH;
              r.Height = BLOCK_WIDTH;
              Canvas.SetLeft(r, j * BLOCK_WIDTH);
              Canvas.SetTop(r, i * BLOCK_WIDTH);
              canvas.Children.Add(r);
            }
          }
          else
          {
            if (map[i, j].type == BlockType.Wall)
            {
              Rectangle r = new Rectangle();
              SolidColorBrush mySolidColorBrush 
                = new SolidColorBrush();
              mySolidColorBrush.Color 
                = Color.FromRgb(255, 255, 255);
              r.Fill = mySolidColorBrush;
              r.StrokeThickness = 1;
              r.Stroke = Brushes.Black;
              r.Width = BLOCK_WIDTH;
              r.Height = BLOCK_WIDTH;
              Canvas.SetLeft(r, j * BLOCK_WIDTH);
              Canvas.SetTop(r, i * BLOCK_WIDTH);
              canvas.Children.Add(r);
            }
            else if (map[i, j].type == BlockType.Point)
            {
              Rectangle r = new Rectangle();
              SolidColorBrush mySolidColorBrush 
                = new SolidColorBrush();
              mySolidColorBrush.Color 
                = Color.FromRgb(255, 0, 0);
              r.Fill = mySolidColorBrush;
              r.StrokeThickness = 1;
              r.Stroke = Brushes.Black;
              r.Width = BLOCK_WIDTH;
              r.Height = BLOCK_WIDTH;
              Canvas.SetLeft(r, j * BLOCK_WIDTH);
              Canvas.SetTop(r, i * BLOCK_WIDTH);
              canvas.Children.Add(r);
            }
            else if (map[i, j].type == BlockType.Empty)
            {
              Rectangle r = new Rectangle();
              SolidColorBrush mySolidColorBrush
                = new SolidColorBrush();
              mySolidColorBrush.Color
                = Color.FromRgb(0, 255, 0);
              r.Fill = mySolidColorBrush;
              r.StrokeThickness = 1;
              r.Stroke = Brushes.Black;
              r.Width = BLOCK_WIDTH;
              r.Height = BLOCK_WIDTH;
              Canvas.SetLeft(r, j * BLOCK_WIDTH);
              Canvas.SetTop(r, i * BLOCK_WIDTH);
              canvas.Children.Add(r);
            }
          }
        }

      if (!game.gameActive)
      {
        TextBlock textBlock = new TextBlock();
        textBlock.Text = "GAME OVER";
        textBlock.FontSize= 24;
        Color color = Color.FromRgb(255, 0, 0);
        textBlock.Foreground = new SolidColorBrush(color);
        Canvas.SetLeft(textBlock, 150);
        Canvas.SetTop(textBlock, 500);
        canvas.Children.Add(textBlock);
      }
    }
  }
}
