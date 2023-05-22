namespace PackageShipping
{
  public class OvernightPackage : Package
  {
    public OvernightPackage(String recipient, string address, double weight,
      DateTime shipmentDate) : base(recipient, address, weight, shipmentDate)
    { }

    public OvernightPackage() : base()
    { }

    public override double CalculateCost()
    {
      return Constants.OVERNIGHT_PACKAGE_COST_FACTOR * Weight;
    }

    public override DateTime CalculateDeliveryDate()
    {
      return ShipmentDate.AddDays(Constants.OVERNIGHT_PACKAGE_DAYS);
    }
  }
}
