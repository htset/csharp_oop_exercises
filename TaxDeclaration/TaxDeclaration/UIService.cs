namespace TaxDeclaration
{
  public class UIService
  {
    private IPersistenceService Ps;
    private StatisticsService Ss;

    public UIService(IPersistenceService ps, StatisticsService ss)
    {
      Ps = ps;
      Ss = ss;
    }

    public void Menu()
    {
      int sel;
      var td = new TaxDeclaration();
      do
      {
        Console.WriteLine("Options: ");
        Console.WriteLine("--Transactions--");
        Console.WriteLine("1: Add new Tax Declaration");
        Console.WriteLine("2: Delete Tax Declaration: ");
        Console.WriteLine("3: Find Tax Declaration: ");
        Console.WriteLine("--Statistics--");
        Console.WriteLine("11: Get total tax");
        Console.WriteLine("12: Get Tax Declaration with highest tax");
        Console.WriteLine("0: exit");

        Console.Write("Your choice: ");
        sel = Convert.ToInt32(Console.ReadLine());

        switch (sel)
        {
          case 1:
            Create();
            break;
          case 2:
            Remove();
            break;
          case 3:
            Search();
            break;
          case 11:
            Console.WriteLine("Total Tax is: " + Ss.GetTotalTax());
            break;
          case 12:
            td = Ss.GetHighestDeclaration();
            Console.WriteLine("Highest Tax Declaration is: ");
            Console.WriteLine(td);
            break;
          default:
            break;
        }
      } while (sel != 0);
    }

    private void Create()
    {
      string sel = "y";
      Console.WriteLine("Enter person details");
      var tax = EnterTaxDeclarationDetails();

      Console.WriteLine("Now enter properties: ");
      while (sel.Equals("y"))
      {
        var p = EnterProperty();
        if (p != null)
        {
          tax.AddProperty(p);
          Console.WriteLine("Property added");
        }
        else
          Console.WriteLine("No property added (user aborted)");

        Console.WriteLine("Would you like to add another property? (y/n)");
        sel = Console.ReadLine() ?? "n";
      }

      Ps.InsertTaxDeclaration(tax);
      Console.WriteLine("Tax declaration added!");
    }

    private void Remove()
    {
      string sel;
      string search_vat;
      int submissionYear;
      Console.Write("Enter VAT number for search: ");
      search_vat = Console.ReadLine() ?? "";
      Console.Write("Enter submission year: ");
      submissionYear = Convert.ToInt32(Console.ReadLine());
      var td = Ps.GetTaxDeclarations(search_vat, submissionYear);

      if (td.Count == 1)
      {
        Console.WriteLine("Found Tax Declaration:");
        Console.WriteLine(td.ElementAt(0).ToString());
        Console.WriteLine("Delete Tax Declaration? (y/n) ");
        sel = Console.ReadLine() ?? "n";

        if (sel.Equals("y"))
        {
          Ps.RemoveTaxDeclaration(td.ElementAt(0));
          Console.WriteLine(" Tax declaration deleted");
        }
        else
        {
          Console.WriteLine(" Tax declaration NOT deleted");
        }
      }
      else
        Console.WriteLine("tax declaration not found");
    }

    private void Search()
    {
      string search_vat;
      int submissionYear;
      Console.Write("Enter VAT number for search (press return for all VATs): ");
      search_vat = Console.ReadLine() ?? "";
      Console.Write("Enter submission year (press 0 for all years): ");
      submissionYear = Convert.ToInt32(Console.ReadLine());

      var td = Ps.GetTaxDeclarations(search_vat, submissionYear);
      Console.WriteLine("-----Tax Declarations-----");
      foreach (var tax in td)
        Console.WriteLine(tax);
    }

    private TaxDeclaration EnterTaxDeclarationDetails()
    {
      string name, surname, vat, tel;
      int year;
      Console.Write("Name: ");
      name = Console.ReadLine() ?? "";
      Console.Write("\nSurname: ");
      surname = Console.ReadLine() ?? "";
      Console.Write("\nVAT number: ");
      vat = Console.ReadLine() ?? "";
      Console.Write("\nTelephone: ");
      tel = Console.ReadLine() ?? "";
      Console.Write("\nFiscal Year: ");
      year = Convert.ToInt32(Console.ReadLine());

      return new TaxDeclaration(0, name, surname, vat, tel, year);
    }

    private Address EnterAddress()
    {
      string street, number, zip, city;
      Console.Write("Street: ");
      street = Console.ReadLine() ?? "";
      Console.WriteLine("\nNumber: ");
      number = Console.ReadLine() ?? "";
      Console.WriteLine("\nZip code: ");
      zip = Console.ReadLine() ?? "";
      Console.WriteLine("\nCity: ");
      city = Console.ReadLine() ?? "";

      return new Address(street, number, zip, city);
    }

    private Property? EnterProperty()
    {
      string sel, inside, cultivated;
      Address address;
      int surface, floor, commerciality;

      Console.Write("Select 1 for Apartment, 2 for Store, 3 for Plot, any other to abort: ");
      sel = Console.ReadLine() ?? "";

      switch (sel)
      {
        case "1":
          address = EnterAddress();
          Console.Write("Surface: ");
          surface = Convert.ToInt32(Console.ReadLine());

          Console.WriteLine("Floor: ");
          floor = Convert.ToInt32(Console.ReadLine());

          return new Apartment(0, surface, address, floor);
        case "2":
          address = EnterAddress();
          Console.Write("Surface: ");
          surface = Convert.ToInt32(Console.ReadLine());

          Console.Write("Commerciality: ");
          commerciality = Convert.ToInt32(Console.ReadLine());

          return new Store(0, surface, address, commerciality);
        case "3":
          address = EnterAddress();
          Console.Write("Surface: ");
          surface = Convert.ToInt32(Console.ReadLine());
          Console.Write("Inside town? ");
          inside = Console.ReadLine() ?? "n";
          Console.Write("Cultivated?");
          cultivated = Console.ReadLine() ?? "n";
          return new Plot(0, surface, address, (inside.Equals("y")) ? true : false, (cultivated.Equals("y")) ? true : false);
        default:
          return null;
      }
    }
  }
}
