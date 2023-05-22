using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PackageShippingGUI
{
  public partial class MainWindow : Window
  {
    private List<Package> Packages;
    private PersistenceService Ps;

    public MainWindow()
    {
      InitializeComponent();

      var c1 = new DataGridTextColumn();
      c1.Header = "Package Type";
      c1.Binding = new Binding("PackageType");
      c1.Width = 110;
      packageGrid.Columns.Add(c1);
      var c2 = new DataGridTextColumn();
      c2.Header = "Recipient";
      c2.Width = 110;
      c2.Binding = new Binding("Recipient");
      packageGrid.Columns.Add(c2);
      var c3 = new DataGridTextColumn();
      c3.Header = "Address";
      c3.Width = 110;
      c3.Binding = new Binding("Address");
      packageGrid.Columns.Add(c3);
      var c4 = new DataGridTextColumn();
      c4.Header = "Weight";
      c4.Width = 110;
      c4.Binding = new Binding("Weight");
      packageGrid.Columns.Add(c4);
      var c5 = new DataGridTextColumn();
      c5.Header = "Shipment Date";
      c5.Width = 110;
      c5.Binding = new Binding("ShipmentDate");
      packageGrid.Columns.Add(c5);
      var c6 = new DataGridTextColumn();
      c6.Header = "Cost";
      c6.Width = 110;
      c6.Binding = new Binding("Cost");
      packageGrid.Columns.Add(c6);
      var c7 = new DataGridTextColumn();
      c7.Header = "Delivery Date";
      c7.Width = 110;
      c7.Binding = new Binding("DeliveryDate");
      packageGrid.Columns.Add(c7);

      Packages = new List<Package>();
      Ps = new PersistenceService(Packages);
      Ps.LoadFromFile();
      PopulateGrid();
    }

    private void PopulateGrid()
    {
      packageGrid.Items.Clear();

      foreach (var p in Packages)
      {
        packageGrid.Items.Add(
            new
            {
              PackageType = p.GetType().Name.Substring(0, p.GetType().Name.IndexOf("Package")),
              Recipient = p.Recipient,
              Address = p.Address,
              Weight = p.Weight,
              ShipmentDate = p.ShipmentDate.ToShortDateString(),
              Cost = p.CalculateCost(),
              DeliveryDate = p.CalculateDeliveryDate().ToShortDateString()
            }
        );
      }
    }

    private void AddPackage_Click(object sender, RoutedEventArgs e)
    {
      var form = new PackageForm();
      form.ShowDialog();
      if (form.package != null)
      {
        Packages.Add(form.package);
        Ps.SaveToFile();
        PopulateGrid();
      }
    }
  }
}
