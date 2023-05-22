using System;
using System.Windows.Input;

namespace Pacman
{
  public enum Direction
  {
    Up, Right, Down, Left
  }

  public class Game
  {
    public Block[,] map { get; set; }
    public int sizeX = 32;
    public int sizeY = 28;
    public bool gameActive = true;
    public Pair pacmanLocation;
    public int pointsLeft;
    public Entity[] player;

    public Game()
    {
      map = new Block[32, 28];
      player = new Entity[4];

      pointsLeft = 0;
      for (int i = 0; i < 32; i++)
      {
        string line = Chart.chart[i];
        for (int j = 0; j < 28; j++)
        {
          map[i, j] = new Block();
          map[i, j].entity = null;

          if (line[j] == '.')
          {
            map[i, j].type = BlockType.Point;
            pointsLeft++;
          }
          else if (line[j] == '*')
            map[i, j].type = BlockType.Wall;
          if (line[j] == ' ')
            map[i, j].type = BlockType.Empty;
        }
      }

      player[0] = new PacMan(this, 23, 13);
      this.map[23, 13].entity = player[0];
      this.pacmanLocation = new Pair(23, 13);

      player[1] = new Ghost(this, 5, 5);
      this.map[5, 5].entity = player[1];

      player[2] = new Ghost(this, 5, 20);
      this.map[5, 20].entity = player[2];

      player[3] = new Ghost(this, 8, 5);
      this.map[8, 5].entity = player[3];
    }

    public void KeyPressed(KeyEventArgs keyEvent)
    {
      Console.Write("Key pressed: " + keyEvent.Key);

      if (keyEvent.Key == Key.Up)
        player[0].direction = Direction.Up;
      if (keyEvent.Key == Key.Right)
        player[0].direction = Direction.Right;
      if (keyEvent.Key == Key.Down)
        player[0].direction = Direction.Down;
      if (keyEvent.Key == Key.Left)
        player[0].direction = Direction.Left;
    }

    public void PlayRound()
    {
      for (int i = 0; i < 4; i++)
        player[i].Play();
    }
  }
}
