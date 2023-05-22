namespace Pacman
{
  public class Pair
  {
    public int x { get; set; }
    public int y { get; set; }

    public Pair(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public Pair()
    {
      this.x = 0;
      this.y = 0;
    }
  }
}
