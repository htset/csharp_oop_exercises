using System.Text;

namespace Insurance
{
  public class AutoInsurance : Insurance
  {
    public int CarAge { get; set; }

    public AutoInsurance(String name, int age, int coverage, int carAge)
      :base(name, age, coverage)
    {
      CarAge = carAge;
    }

    public AutoInsurance() : base() { }

    public override double CalculateCost()
    {
      return -Age + 0.05 * Coverage + 10 * CarAge;
    }

    public override string ToString()
    {
      var builder = new StringBuilder();
      builder.Append("\n------Auto Insurance policy------");
      builder.Append("\nName: " + Name);
      builder.Append("\nAge: " + Age);
      builder.Append("\nCoverage: " + Coverage);
      builder.Append("\nCar Age: " + CarAge);
      builder.Append("\nYearly cost: " + CalculateCost());

      return builder.ToString();
    }
  }
}
