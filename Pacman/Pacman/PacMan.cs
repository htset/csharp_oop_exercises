using System.Collections.Generic;

namespace Pacman
{
  public class PacMan : Entity
  {
    public PacMan(Game map, int x, int y)
    {
      this.game = map;
      this.type = EntityType.Pacman;
      this.x = x;
      this.y = y;
    }

    public override void Play()
    {
      var candidateBlocks = new List<Pair>();
      for (int i = x - 1; i <= x + 1; i++)
        for (int j = y - 1; j <= y + 1; j++)
        {
          if (i >= 0 && i < game.sizeX
              && j >= 0 && j < game.sizeY
              && !(i == x && j == y)
              && game.map[i, j].type != BlockType.Wall)
          {
            candidateBlocks.Add(new Pair(i, j));
          }
        }

      if (direction == Direction.Up
          && (candidateBlocks
          .Find(p => (p.x == x - 1 && p.y == y))) != null)
      {
        if (game.map[x-1, y].entity != null)
          game.gameActive = false;
        Move(x - 1, y);
      }
      else if (direction == Direction.Right
          && (candidateBlocks
          .Find(p => (p.x == x && p.y == y + 1))) != null)
      {
        if (game.map[x, y+1].entity != null)
          game.gameActive = false;
        Move(x, y + 1);
      }
      else if (direction == Direction.Down
          && (candidateBlocks
          .Find(p => (p.x == x + 1 && p.y == y))) != null)
      {
        if (game.map[x + 1, y].entity != null)
          game.gameActive = false;
        Move(x + 1, y);
      }
      else if (direction == Direction.Left
          && (candidateBlocks
          .Find(p => (p.x == x && p.y == y - 1))) != null)
      {
        if (game.map[x, y-1].entity != null)
          game.gameActive = false;
        Move(x, y - 1);
      }
      
      game.pacmanLocation.x = x;
      game.pacmanLocation.y = y;

      if (game.map[x, y].type == BlockType.Point)
      {
        game.map[x, y].type = BlockType.Empty;
        game.pointsLeft--;
        if (game.pointsLeft == 0)
          game.gameActive = false;
      }
    }
  }
}
