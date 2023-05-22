namespace PackageShipping
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var packages = new Package[4];

      packages[0] = new BasePackage("John Doe", "12 Main str.", 15,
          new DateTime(2023, 1, 12));
      packages[1] = new BasePackage("Jane Doe", "1 High str.", 9.5,
          new DateTime(2022, 12, 30));
      packages[2] = new AdvancedPackage("Janet Doe", "3 Square dr.", 15,
          new DateTime(2023, 1, 12));
      packages[3] = new OvernightPackage("James Doe", "12 Infinite loop", 1,
          new DateTime(2023, 1, 20));

      int i = 1;
      foreach (var p in packages)
      {
        Console.WriteLine("Package no." + i++);
        Console.WriteLine("Type: " + p.GetType());
        Console.WriteLine("Delivery date: " + p.CalculateDeliveryDate());
        Console.WriteLine("Cost: " + p.CalculateCost() + "\n");
      }
    }
  }
}