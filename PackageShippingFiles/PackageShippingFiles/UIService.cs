namespace PackageShipping
{
  public class UIService
  {
    private List<Package> Packages;

    public UIService(List<Package> packages)
    {
      Packages = packages;
    }

    public void Menu()
    {
      int option;
      do
      {
        Console.WriteLine("Options: ");
        Console.WriteLine("1) Add package ");
        Console.WriteLine("2) Search package ");
        Console.WriteLine("3) Delete package ");
        Console.WriteLine("4) View all packages ");
        Console.WriteLine("0) Exit ");

        int.TryParse(Console.ReadLine(), out option);

        switch (option)
        {
          case 1:
            AddPackage();
            break;
          case 2:
            SearchPackage();
            break;
          case 3:
            DeletePackage();
            break;
          case 4:
            ListPackages();
            break;
          case 0:
            break;
          default:
            Console.WriteLine("Please type again:");
            break;
        }
      } while (option != 0);
    }

    private void AddPackage()
    {
      int option;
      string? recipient_name, recipient_address;
      double weight;

      Console.WriteLine("Type of package (1-Basic, 2-Advanced, 3-Overnight)");
      option = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine("Recipient name: ");
      recipient_name = Console.ReadLine() ?? "";
      Console.WriteLine("Recipient address: ");
      recipient_address = Console.ReadLine() ?? "";
      Console.WriteLine("Weight (kilos):");
      weight = Convert.ToDouble(Console.ReadLine());
      switch (option)
      {
        case 1:
          Packages.Add(new BasePackage(recipient_name, recipient_address,
              weight, DateTime.Now));
          SaveToFile();
          break;
        case 2:
          Packages.Add(new AdvancedPackage(recipient_name, recipient_address,
              weight, DateTime.Now));
          SaveToFile();
          break;
        case 3:
          Packages.Add(new OvernightPackage(recipient_name, recipient_address,
              weight, DateTime.Now));
          SaveToFile();
          break;
        default:
          break;
      }
    }

    private void SearchPackage()
    {
      string? recipient_name;
      Console.WriteLine("Enter recipient name (also partial): ");
      recipient_name = Console.ReadLine() ?? "";

      foreach (var p in Packages)
      {
        if (p.Recipient.IndexOf(recipient_name) >= 0)
        {
          Console.WriteLine(p);
        }
      }
    }

    private void DeletePackage()
    {
      string recipient_name;
      Console.Write("Enter recipient name (also partial): ");
      recipient_name = Console.ReadLine() ?? "";

      int i = 0;
      Console.WriteLine("The following packages were found:");
      foreach (var p in Packages)
      {
        if (p.Recipient.IndexOf(recipient_name) >= 0)
        {
          Console.WriteLine("Package no. " + (i + 1) + ":");
          Console.WriteLine(p);
        }
        i++;
      }

      int option;
      Console.WriteLine("Please enter the no. of package to delete (0 to cancel): ");
      option = Convert.ToInt32(Console.ReadLine());

      if (option > 0)
      {
        Packages.RemoveAt(option - 1);
        Console.WriteLine("package deleted");
        SaveToFile();
      }
    }

    private void ListPackages()
    {
      foreach (var p in Packages)
      {
        Console.WriteLine(p);
      }
    }

    private void SaveToFile()
    {
      using (var writer = new StreamWriter("packages.txt"))
      {
        foreach (var p in Packages)
        {
          writer.WriteLine(p.Serialize());
        }
      }
    }

    public void LoadFromFile()
    {
      Packages.Clear();

      if (!File.Exists("packages.txt"))
      {
        var newFile = File.Create("packages.txt");
        newFile.Close();
      }

      using (var reader = new StreamReader("packages.txt"))
      {
        string line;
        string? package_str = "";
        while ((line = reader.ReadLine()) != null)
        {
          if (line.IndexOf("--") == 0)
          {
            Package p;
            if (package_str.IndexOf("Base Package") == 0)
            {
              p = new BasePackage();
              p.Deserialize(package_str);
              Packages.Add(p);
            }
            else if (package_str.IndexOf("Advanced Package") == 0)
            {
              p = new AdvancedPackage();
              p.Deserialize(package_str);
              Packages.Add(p);
            }
            else if (package_str.IndexOf("Overnight Package") == 0)
            {
              p = new OvernightPackage();
              p.Deserialize(package_str);
              Packages.Add(p);
            }
            else
            {
              Console.WriteLine("error loading packages. Exiting..");
              System.Environment.Exit(1);
            }
            package_str = "";
          }
          else
          {
            package_str += line + "\n";
          }

        }
      }
    }
  }
}
