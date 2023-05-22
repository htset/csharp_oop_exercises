using System.Text.Json;

namespace TaxDeclaration
{
  public class FileService : IPersistenceService
  {
    List<TaxDeclaration> Declarations;
    string Filename;
    private readonly JsonSerializerOptions Options
        = new()
        {
          Converters = { new PropertyConverter() },
          WriteIndented = true,
        };

    public FileService(string filename)
    {
      Filename = filename;
      Declarations = new List<TaxDeclaration>();
      LoadFromFile();
    }

    public void InsertTaxDeclaration(TaxDeclaration td)
    {
      Declarations.Add(td);
      SaveToFile();
    }

    public void RemoveTaxDeclaration(TaxDeclaration declaration)
    {
      foreach (var td in Declarations)
      {
        if (td.Vat.Equals(declaration.Vat) 
          && td.SubmissionYear == declaration.SubmissionYear)
        {
          Declarations.Remove(td);
          break;
        }
      }
      SaveToFile();
    }

    public List<TaxDeclaration> GetTaxDeclarations(string vat, int submissionYear)
    {
      if (vat.Trim().Equals("") && submissionYear == 0)
        return Declarations;

      var ret = new List<TaxDeclaration>();
      foreach (var td in Declarations)
      {
        if (!vat.Equals(""))
        {
          if (submissionYear != 0)
          {
            if (td.Vat.Equals(vat) && td.SubmissionYear == submissionYear)
              ret.Add(td);
          }
          else
          {
            if (td.Vat.Equals(vat))
              ret.Add(td);
          }
        }
        else
        {
          if (submissionYear != 0)
          {
            if (td.SubmissionYear == submissionYear)
              ret.Add(td);
          }
        }
      }
      return ret;
    }

    private void SaveToFile()
    {
      using (StreamWriter writer = new StreamWriter(Filename))
      {
        writer.Write(JsonSerializer.Serialize<TaxDeclaration[]>(Declarations.ToArray(), Options));
      }
    }

    public void LoadFromFile()
    {
      Declarations.Clear();

      if (!File.Exists(Filename))
      {
        var newFile = File.Create(Filename);
        newFile.Close();
      }

      string json = File.ReadAllText(Filename);
      if (json != null && json.Length > 0)
        Declarations = (JsonSerializer.Deserialize<TaxDeclaration[]>(json, Options)).ToList();
    }
  }
}
