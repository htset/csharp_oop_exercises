namespace PackageShipping
{
  public class BasePackage : Package
  {
    public BasePackage(string recipient, string address, double weight,
      DateTime shipmentDate) : base(recipient, address, weight, shipmentDate)
    { }

    public BasePackage() : base()
    { }

    public override double CalculateCost()
    {
      return Constants.BASE_PACKAGE_COST_FACTOR * Weight;
    }

    public override DateTime CalculateDeliveryDate()
    {
      if (Weight <= Constants.BASE_PACKAGE_MAX_WEIGHT)
        return ShipmentDate.AddDays(Constants.BASE_PACKAGE_DAYS);
      else
        return ShipmentDate.AddDays(Constants.BASE_PACKAGE_DAYS + 1);
    }
  }
}
