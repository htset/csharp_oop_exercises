namespace Insurance
{
  public abstract class Insurance
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public int Coverage { get; set; }

    public Insurance(string name, int age, int coverage)
    {
      Name = name;
      Age = age;
      Coverage = coverage;
    }

    public Insurance()
    {
      Name = "N/A";
      Age = 0;
      Coverage = 0;
    }

    public abstract double CalculateCost();
  }
}
