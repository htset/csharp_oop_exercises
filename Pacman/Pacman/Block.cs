namespace Pacman
{
  public enum BlockType
  {
    Wall, Point, Empty
  }

  public class Block
  {
    public BlockType type { get; set; }
    public Entity? entity { get; set; }
  }
}
