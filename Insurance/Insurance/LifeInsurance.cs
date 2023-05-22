using System.Text;

namespace Insurance
{
  public class LifeInsurance : Insurance
  {
    public LifeInsurance(string name, int age, int coverage) 
      :base(name, age, coverage) { }

    public LifeInsurance() : base() { }

    public override double CalculateCost()
    {
      return 10 * Age + 0.001 * Coverage;
    }

    public override string ToString()
    {
      var builder = new StringBuilder();
      builder.Append("\n------Life Insurance policy------");
      builder.Append("\nName: " + Name);
      builder.Append("\nAge: " + Age);
      builder.Append("\nCoverage: " + Coverage);
      builder.Append("\nYearly cost: " + CalculateCost());

      return builder.ToString();
    }
  }
}
