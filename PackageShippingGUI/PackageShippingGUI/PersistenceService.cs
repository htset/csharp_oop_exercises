using System;
using System.Collections.Generic;
using System.IO;

namespace PackageShippingGUI
{
  public class PersistenceService
  {
    private List<Package> Packages;

    public PersistenceService(List<Package> packages)
    {
      Packages = packages;
    }

    public void SaveToFile()
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
