namespace Insurance
{
  public class Program
  {
    static void SaveInsurance(Insurance insurance)
    {
      using (var writer = new StreamWriter("insurance.txt", true))
      {
        writer.WriteLine(insurance.ToString());

        //this will not compile
        //writer.WriteLine(insurance.CarAge);

        //this will work
        if (insurance.GetType().ToString().Equals("class AutoInsurance"))
          writer.WriteLine(">>>Car age: " + ((AutoInsurance)insurance).CarAge);
      }
    }
    static void Main(string[] args)
    {
      AutoInsurance i1 = new AutoInsurance("John Doe", 30, 15000, 2);
      Console.Write(i1);
      LifeInsurance i2 = new LifeInsurance("John Doe", 30, 1000000);
      Console.Write(i2);

      SaveInsurance(i1);
      SaveInsurance(i2);
    }
  }
}