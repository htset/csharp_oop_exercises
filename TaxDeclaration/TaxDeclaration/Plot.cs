namespace TaxDeclaration
{
  public class Plot : Property
  {
    public bool WithinCityLimits { get; set; }
    public bool Cultivated { get; set; }

    public Plot(int id, int surface, Address address, bool withinCityLimits, 
      bool cultivated) : base(id, surface, address)
    {
      this.WithinCityLimits = withinCityLimits;
      this.Cultivated = cultivated;
    }

    public Plot() : base() { }

    public override string ToString()
    {
      return "Plot{" +
          "withinCityLimits=" + WithinCityLimits +
          ", cultivated=" + Cultivated +
          ", id=" + Id +
          ", surface=" + Surface +
          ", address=" + Address +
          '}';
    }

    public override double CalculateTax()
    {
      return (0.3 * Surface + 100 * (Cultivated ? 1 : 0) 
        + 200 * (WithinCityLimits ? 1 : 0));
    }
  }
}
