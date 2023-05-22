using System.IO;

namespace TaxDeclaration
{
  public class Address
  {
    public string Street { get; set; }
    public string No { get; set; }
    public string Zip { get; set; }
    public string City { get; set; }

    public Address(string street, string no, string zip, string city)
    {
      Street = street;
      No = no;
      Zip = zip;
      City = city;
    }

    public Address()
    {
      Street = "N/A";
      No = "N/A";
      Zip = "N/A";
      City = "N/A";
    }

    public override string ToString()
    {
      return "Address{" +
          "street='" + Street + '\'' +
          ", number='" + No + '\'' +
          ", zip='" + Zip + '\'' +
          ", city='" + City + '\'' +
          '}';
    }
  }
}