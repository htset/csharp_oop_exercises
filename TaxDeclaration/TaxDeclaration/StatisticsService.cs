namespace TaxDeclaration
{
  public class StatisticsService
  {
    IPersistenceService Ps;

    public StatisticsService(IPersistenceService ps)
    {
      Ps = ps;
    }

    public double GetTotalTax()
    {
      double totalTax = 0;
      foreach (var td in Ps.GetTaxDeclarations("", 0))
      {
        totalTax += td.CalculateTax();
      }
      return totalTax;
    }

    public TaxDeclaration? GetHighestDeclaration()
    {
      var declarations = Ps.GetTaxDeclarations("", 0);

      if (declarations.Count > 0)
      {
        var max = declarations.ElementAt(0);
        double highestTax = 0;
        foreach (var td in declarations)
        {
          if (td.CalculateTax() > highestTax)
          {
            max = td;
            highestTax = td.CalculateTax();
          }
        }
        return max;
      }
      else
        return null;
    }
  }
}
