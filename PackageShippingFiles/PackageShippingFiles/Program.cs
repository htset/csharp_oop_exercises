namespace PackageShipping
{
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        var list = new List<Package>();
        var ui = new UIService(list);
        ui.LoadFromFile();
        ui.Menu();
      }
      catch (IOException ioe)
      {
        Console.WriteLine("IO Exception!");
        Console.WriteLine(ioe.StackTrace);
        System.Environment.Exit(1);
      }
    }
  }
}