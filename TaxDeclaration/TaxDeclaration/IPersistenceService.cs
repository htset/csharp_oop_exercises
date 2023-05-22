namespace TaxDeclaration
{
  public interface IPersistenceService
  {
    void InsertTaxDeclaration(TaxDeclaration td);
    void RemoveTaxDeclaration(TaxDeclaration td);
    List<TaxDeclaration> GetTaxDeclarations(string vat, int year);
  }
}
