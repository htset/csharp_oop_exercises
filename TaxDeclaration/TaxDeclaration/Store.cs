namespace TaxDeclaration
{
  public class Store : Property
  {
    public int Commerciality;

    public Store(int id, int surface, Address address, int commerciality) : base(id, surface, address)
    {
      Commerciality = commerciality;
    }

    public Store() : base() { }

    public override string ToString()
    {
      return "Store{" +
          "commerciality=" + Commerciality +
          ", id=" + Id +
          ", surface=" + Surface +
          ", address=" + Address +
          '}';
    }

    public override double CalculateTax()
    {
      return (2.5 * Surface + 20 * Commerciality + 100);
    }
  }
}
