using System;
using System.Windows;

namespace PackageShippingGUI
{
  public partial class PackageForm : Window
  {
    public Package? package { get; set; }

    public PackageForm()
    {
      InitializeComponent();

      PackageType.Items.Clear();
      PackageType.Items.Add("Base");
      PackageType.Items.Add("Advanced");
      PackageType.Items.Add("Overnight");
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      switch (PackageType.SelectedValue)
      {
        case "Base":
          package = new BasePackage();
          package.Recipient = Recipient.Text;
          package.Address = Address.Text;
          package.Weight = Int32.Parse(Weight.Text);
          package.ShipmentDate = ShipmentDate.SelectedDate ?? DateTime.Now;
          break;

        case "Advanced":
          package = new AdvancedPackage();
          package.Recipient = Recipient.Text;
          package.Address = Address.Text;
          package.Weight = Int32.Parse(Weight.Text);
          package.ShipmentDate = ShipmentDate.SelectedDate ?? DateTime.Now;
          break;

        case "Overnight":
          package = new OvernightPackage();
          package.Recipient = Recipient.Text;
          package.Address = Address.Text;
          package.Weight = Int32.Parse(Weight.Text);
          package.ShipmentDate = ShipmentDate.SelectedDate ?? DateTime.Now;
          break;
      }
      this.Close();
    }
  }
}
