namespace Reservations
{
  public class Program
  {
    static void Main(string[] args)
    {
      var ps = new PersistenceService();
      var ui = new UIService(ps);
      ui.Menu();
    }
  }
}