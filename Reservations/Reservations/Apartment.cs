namespace Reservations
{
  public class Apartment
  {
    public int Id { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }

    public Apartment(int id, string address, int capacity, decimal price)
    {
      Id = id;
      Address = address;
      Capacity = capacity;
      Price = price;
    }

    public override string ToString()
    {
      return "--Apartment--" +
          "\nid = " + Id +
          "\naddress = " + Address +
          "\ncapacity = " + Capacity +
          "\nprice = " + Price;
    }
  }
}
