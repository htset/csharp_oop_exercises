namespace PackageShipping
{
  public class AdvancedPackage : Package
  {
    public AdvancedPackage(String recipient, string address, double weight,
      DateTime shipmentDate) : base(recipient, address, weight, shipmentDate)
    { }

    public AdvancedPackage() : base()
    { }

    public override double CalculateCost()
    {
      return Constants.ADVANCED_PACKAGE_COST_FACTOR * Weight
          + Constants.ADVANCED_PACKAGE_COST_SUPPL;
    }

    public override DateTime CalculateDeliveryDate()
    {
      return ShipmentDate.AddDays(Constants.ADVANCED_PACKAGE_DAYS);
    }
  }
}
