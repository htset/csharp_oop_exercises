namespace TaxDeclaration
{
  public class Program
  {
    static void Main(string[] args)
    {
      IPersistenceService ps = new DBService();
      //IPersistenceService ps = new FileService("td.json");
      StatisticsService ss = new StatisticsService(ps);
      UIService ui = new UIService(ps, ss);
      ui.Menu();
    }
  }
}