using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PackageShippingGUI
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

    public override string ToString()
    {
      var str = new StringBuilder();
      str.Append("\n--Base Package--");
      str.Append("\nRecipient: " + Recipient);
      str.Append("\nAddress: " + Address);
      str.Append("\nWeight: " + Weight);
      str.Append("\nShipment date:" + ShipmentDate);
      str.Append("\nExpected delivery date:" + CalculateDeliveryDate());
      str.Append("\nCost:" + CalculateCost());
      str.Append("\n---------------------");
      return str.ToString();
    }

    public override string Serialize()
    {
      var str = new StringBuilder();
      str.Append("Base Package\n");
      str.Append(Recipient + "\n");
      str.Append(Address + "\n");
      str.Append(Weight + "\n");
      str.Append(ShipmentDate + "\n");
      str.Append("--");
      return str.ToString();
    }

    public override void Deserialize(string s)
    {
      var regex = new Regex("\n");
      var lines = regex.Split(s);

      Recipient = lines[1];
      Address = lines[2];
      Weight = Double.Parse(lines[3]);
      ShipmentDate = DateTime.Parse(lines[4]);
    }
  }
}
