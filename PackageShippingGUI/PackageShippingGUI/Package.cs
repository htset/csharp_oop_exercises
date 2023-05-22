using System;

namespace PackageShippingGUI
{
  public abstract class Package
  {
    private double _weight;
    public string Recipient { get; set; }
    public string Address { get; set; }
    public double Weight
    {
      get => _weight;
      set
      {
        if (value < 0)
          throw new Exception("Weight should be >= 0");
        else
          _weight = value;
      }
    }
    public DateTime ShipmentDate { get; set; }

    public Package(string recipient, string address, double weight,
      DateTime shipmentDate)
    {
      Recipient = recipient;
      Address = address;
      Weight = weight;
      ShipmentDate = shipmentDate;
    }

    public Package()
    {
      Recipient = "";
      Address = "";
      Weight = 0;
      ShipmentDate = DateTime.Now;
    }

    public abstract double CalculateCost();
    public abstract DateTime CalculateDeliveryDate();
    public override abstract string ToString();
    public abstract string Serialize();
    public abstract void Deserialize(string s);
  }
}
