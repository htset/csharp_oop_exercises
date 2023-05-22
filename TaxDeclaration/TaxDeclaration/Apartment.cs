namespace TaxDeclaration
{
  public class Apartment : Property
  {
    public int Floor { get; set; }

    public Apartment(int id, int surface, Address address, int floor) : base(id, surface, address)
    {
      Floor = floor;
    }

    public Apartment() : base() { }

    public override string ToString()
    {
      return "Apartment{" +
          "floor=" + Floor +
          ", id=" + Id +
          ", surface=" + Surface +
          ", address=" + Address +
          '}';
    }

    public override double CalculateTax()
    {
      return (1.3 * Surface + 10 * Floor + 150);
    }
  }
}
