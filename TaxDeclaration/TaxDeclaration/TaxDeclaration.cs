using System.Numerics;
using System.Xml.Linq;

namespace TaxDeclaration
{
  public class TaxDeclaration
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Vat { get; set; }
    public string Phone { get; set; }
    public int SubmissionYear { get; set; }
    public List<Property> Properties { get; set; }

    public TaxDeclaration(int id, string name, string surname, string vat, string phone, int submissionYear)
    {
      Id = id;
      Name = name;
      Surname = surname;
      Vat = vat;
      Phone = phone;
      SubmissionYear = submissionYear;
      Properties = new List<Property>();
    }

    public TaxDeclaration()
    {
      Id = 0;
      Name = "N/A";
      Surname = "N/A";
      Vat = "N/A";
      Phone = "N/A";
      SubmissionYear = 0;
      Properties = new List<Property>();
    }

    public override string ToString()
    {
      return "TaxDeclaration{" +
          "id=" + Id +
          ", name='" + Name + '\'' +
          ", surname='" + Surname + '\'' +
          ", vat='" + Vat + '\'' +
          ", phone='" + Phone + '\'' +
          ", submissionYear=" + SubmissionYear +
          ", properties=" + Properties +
          '}';
    }

    public void AddProperty(Property p)
    {
      Properties.Add(p);
    }

    public double CalculateTax()
    {
      double totalTax = 0;
      foreach (var prop in Properties)
      {
        totalTax += prop.CalculateTax();
      }
      return totalTax;
    }
  }
}
