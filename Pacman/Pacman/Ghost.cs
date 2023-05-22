using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
  public class Ghost : Entity
  {
    public Ghost(Game map, int x, int y)
    {
      this.game = map;
      this.type = EntityType.Ghost;
      this.x = x;
      this.y = y;
    }

    public override void Play()
    {
      var candidateBlocks = new List<Pair>();
      var distanceToPacman = new List<double>();

      for (int i = x - 1; i <= x + 1; i++)
        for (int j = y - 1; j <= y + 1; j++)
        {
          if (i >= 0 && i < game.sizeX
              && j >= 0 && j < game.sizeY
              && !(i == x && j == y)
              && game.map[i, j].type != BlockType.Wall)
          {
            candidateBlocks.Add(new Pair(i, j));
            distanceToPacman
                .Add(Math.Sqrt(Math.Pow(i - game.pacmanLocation.x, 2)
                        + Math.Pow(j - game.pacmanLocation.y, 2)));
          }
        }

      int minDistIn = distanceToPacman.IndexOf(distanceToPacman.Min());

      if (game.map[candidateBlocks.ElementAt(minDistIn).x, 
        candidateBlocks.ElementAt(minDistIn).y]
          .entity != null)
      {
        //move only if pacman is there
        if (game.map[candidateBlocks.ElementAt(minDistIn).x, 
          candidateBlocks.ElementAt(minDistIn).y]
            .entity.type == EntityType.Pacman)
        {
          //eat pacman
          game.map[candidateBlocks.ElementAt(minDistIn).x, 
            candidateBlocks.ElementAt(minDistIn).y]
              .entity = null;
          game.gameActive = false;
          Move(candidateBlocks.ElementAt(minDistIn).x, 
            candidateBlocks.ElementAt(minDistIn).y);
        }
      }
      else
      {
        Move(candidateBlocks.ElementAt(minDistIn).x, 
          candidateBlocks.ElementAt(minDistIn).y);
      }
    }
  }
}
