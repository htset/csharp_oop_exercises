namespace TaxDeclaration
{
  public abstract class Property
  {
    public int Id { get; set; }
    public int Surface { get; set; }
    public Address Address { get; set; }

    public Property(int id, int surface, Address address)
    {
      Id = id;
      Surface = surface;
      Address = address;
    }

    public Property()
    {
      Id = 0;
      Surface= 0;
      Address = new Address();
    }

    public abstract double CalculateTax();
  }
}
