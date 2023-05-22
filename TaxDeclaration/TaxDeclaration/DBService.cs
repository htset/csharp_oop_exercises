using System.Data;
using System.Data.SqlClient;

namespace TaxDeclaration
{
  public class DBService : IPersistenceService
  {
    SqlConnection Connection;

    public DBService()
    {
      try
      {
        string connectionString;
        connectionString = @"Server=localhost\SQLEXPRESS;Database=TaxDeclarations;Trusted_Connection=True;MultipleActiveResultSets=true;";
        Connection = new SqlConnection(connectionString);
        Connection.Open();
      }
      catch (SqlException e)
      {
        Console.WriteLine("Could not connect to database. Exiting..");
        Console.WriteLine(e.StackTrace);
        System.Environment.Exit(1);
      }
    }

    public List<TaxDeclaration> GetTaxDeclarations(String vat, int year)
    {
      var result = new List<TaxDeclaration>();
      try
      {
        using (var cmd = new SqlCommand())
        {
          cmd.Connection = Connection;

          if (vat != "")
          {
            if (year != 0)
            {
              cmd.CommandText = @"SELECT * from TaxDeclarations 
                WHERE vat = @vat and submissionYear = @submissionYear ";
              cmd.Parameters.Add("@vat", SqlDbType.NVarChar, 20).Value = vat;
              cmd.Parameters.Add("@submissionYear", SqlDbType.Int).Value = year;
            }
            else
            {
              cmd.CommandText = @"SELECT * from TaxDeclarations WHERE vat = @vat ";
              cmd.Parameters.Add("@vat", SqlDbType.NVarChar, 20).Value = vat;
            }
          }
          else
          {
            if (year != 0)
            {
              cmd.CommandText = @"SELECT * from TaxDeclarations 
                WHERE submissionYear = @submissionYear ";
              cmd.Parameters.Add("@submissionYear", SqlDbType.Int).Value = year;
            }
            else
            {
              cmd.CommandText = @"SELECT * from TaxDeclarations ";
            }
          }

          cmd.Prepare();

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              TaxDeclaration td = new TaxDeclaration(
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("name")),
                reader.GetString(reader.GetOrdinal("surname")),
                reader.GetString(reader.GetOrdinal("vat")),
                reader.GetString(reader.GetOrdinal("phone")),
                reader.GetInt32(reader.GetOrdinal("submissionYear"))
              );

              using (var cmdProperty = new SqlCommand())
              {
                cmdProperty.Connection = Connection;
                cmdProperty.CommandText = @"SELECT * from Apartments 
                  WHERE taxDeclarationId = @taxDeclarationId";
                cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
                  = reader.GetInt32(reader.GetOrdinal("id"));

                using (SqlDataReader readerProperty = cmdProperty.ExecuteReader())
                {
                  while (readerProperty.Read())
                  {
                    using (var cmdAddress = new SqlCommand())
                    {
                      cmdAddress.Connection = Connection;
                      cmdAddress.CommandText = @"SELECT * from Addresses 
                        where propertyId = @propertyId";
                      cmdAddress.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                        = readerProperty.GetInt32(readerProperty.GetOrdinal("id"));

                      using (SqlDataReader readerAddress = cmdAddress.ExecuteReader())
                      {
                        while (readerProperty.Read())
                        {
                          Address addr = new Address(
                            readerAddress.GetString(readerAddress.GetOrdinal("street")),
                            readerAddress.GetString(readerAddress.GetOrdinal("number")),
                            readerAddress.GetString(readerAddress.GetOrdinal("zip")),
                            readerAddress.GetString(readerAddress.GetOrdinal("city"))
                          );

                          Apartment ap = new Apartment(
                            readerProperty.GetInt32(readerProperty.GetOrdinal("id")),
                            readerProperty.GetInt32(readerProperty.GetOrdinal("surface")),
                            addr,
                            readerProperty.GetInt32(readerProperty.GetOrdinal("floor"))
                          );

                          td.AddProperty(ap);
                        }
                      }
                    }
                  }
                }
              }

              using (var cmdProperty = new SqlCommand())
              {
                cmdProperty.Connection = Connection;
                cmdProperty.CommandText = @"SELECT * from Stores 
                  WHERE taxDeclarationId = @taxDeclarationId";
                cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
                  = reader.GetInt32(reader.GetOrdinal("id"));

                using (SqlDataReader readerProperty = cmdProperty.ExecuteReader())
                {
                  while (readerProperty.Read())
                  {
                    using (var cmdAddress = new SqlCommand())
                    {
                      cmdAddress.Connection = Connection;
                      cmdAddress.CommandText = @"SELECT * from Addresses 
                        WHERE propertyId = @propertyId";
                      cmdAddress.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                        = readerProperty.GetInt32(reader.GetOrdinal("id"));

                      using (SqlDataReader readerAddress = cmdAddress.ExecuteReader())
                      {
                        while (readerProperty.Read())
                        {
                          Address addr = new Address(
                            readerAddress.GetString(readerAddress.GetOrdinal("street")),
                            readerAddress.GetString(readerAddress.GetOrdinal("number")),
                            readerAddress.GetString(readerAddress.GetOrdinal("zip")),
                            readerAddress.GetString(readerAddress.GetOrdinal("city"))
                          );

                          Store st = new Store(
                            readerProperty.GetInt32(readerProperty.GetOrdinal("id")),
                            readerProperty.GetInt32(readerProperty.GetOrdinal("surface")),
                            addr,
                            readerProperty.GetInt32(readerProperty.GetOrdinal("commerciality"))
                          );

                          td.AddProperty(st);
                        }
                      }
                    }
                  }
                }
              }

              using (var cmdProperty = new SqlCommand())
              {
                cmdProperty.Connection = Connection;
                cmdProperty.CommandText = @"SELECT * from Plots 
                  WHERE taxDeclarationId = @taxDeclarationId";
                cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
                  = reader.GetInt32(reader.GetOrdinal("id"));

                using (SqlDataReader readerProperty = cmdProperty.ExecuteReader())
                {
                  while (readerProperty.Read())
                  {
                    using (var cmdAddress = new SqlCommand())
                    {
                      cmdAddress.Connection = Connection;
                      cmdAddress.CommandText = @"SELECT * from Addresses 
                        WHERE propertyId = @propertyId";
                      cmdAddress.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                        = readerProperty.GetInt32(reader.GetOrdinal("id"));

                      using (SqlDataReader readerAddress = cmdAddress.ExecuteReader())
                      {
                        while (readerProperty.Read())
                        {
                          Address addr = new Address(
                            readerAddress.GetString(readerAddress.GetOrdinal("street")),
                            readerAddress.GetString(readerAddress.GetOrdinal("number")),
                            readerAddress.GetString(readerAddress.GetOrdinal("zip")),
                            readerAddress.GetString(readerAddress.GetOrdinal("city"))
                          );

                          Plot pl = new Plot(
                            readerProperty.GetInt32(readerProperty.GetOrdinal("id")),
                            readerProperty.GetInt32(readerProperty.GetOrdinal("surface")),
                            addr,
                            readerProperty.GetBoolean(readerProperty.GetOrdinal("cultivated")),
                            readerProperty.GetBoolean(readerProperty.GetOrdinal("withinCityLimits"))
                          );
                          td.AddProperty(pl);
                        }
                      }
                    }
                  }
                }
              }
              result.Add(td);
            }
          }

        }
      }
      catch (SqlException e)
      {
        Console.Write("Database error. Exiting..");
        Console.Write(e.StackTrace);
        this.Close();
        System.Environment.Exit(1);
      }
      return result;
    }

    public void InsertTaxDeclaration(TaxDeclaration td)
    {
      using (var transaction = Connection.BeginTransaction())
      {
        try
        {
          using (var cmd = new SqlCommand())
          {
            cmd.Connection = Connection;
            cmd.Transaction = transaction;
            cmd.CommandText 
              = @"insert into TaxDeclarations(name, surname, vat, phone, submissionYear) 
              OUTPUT INSERTED.ID values(@name, @surname, @vat, @phone, @submissionYear)";
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value 
              = td.Name;
            cmd.Parameters.Add("@surname", SqlDbType.NVarChar, 50).Value
              = td.Surname;
            cmd.Parameters.Add("@vat", SqlDbType.NVarChar, 20).Value 
              = td.Vat;
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value 
              = td.Phone;
            cmd.Parameters.Add("@submissionYear", SqlDbType.Int).Value 
              = td.SubmissionYear;
            cmd.Prepare();

            var newId = (int)cmd.ExecuteScalar();

            if (newId > 0)
            {
              var properties = td.Properties;

              using (var cmdProperty = new SqlCommand())
              {
                cmdProperty.Connection = Connection;
                cmdProperty.Transaction = transaction;

                foreach (Property p in properties)
                {
                  if (p is Apartment)
                  {
                    Apartment temp = (Apartment)p;

                    cmdProperty.CommandText 
                      = @"insert into Apartments(surface, floor, taxDeclarationId)
                      OUTPUT INSERTED.ID values(@surface, @floor, @taxDeclarationId)";
                    cmdProperty.Parameters.Add("@surface", SqlDbType.Int).Value 
                      = temp.Surface;
                    cmdProperty.Parameters.Add("@floor", SqlDbType.Int).Value 
                      = temp.Floor;
                    cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
                      = newId;
                    cmdProperty.Prepare();

                    var newPropertyId = (int)cmdProperty.ExecuteScalar();

                    cmdProperty.CommandText 
                      = @"insert into Addresses(street, number, zip, city, propertyId) 
                      values(@street, @number, @zip, @city, @propertyId)";
                    cmdProperty.Parameters.Add("@street", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Street;
                    cmdProperty.Parameters.Add("@number", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.No;
                    cmdProperty.Parameters.Add("@zip", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Zip;
                    cmdProperty.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.City;
                    cmdProperty.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                      = newPropertyId;
                    cmdProperty.Prepare();
                    cmdProperty.ExecuteNonQuery();
                  }
                  else if (p is Store)
                  {
                    Store temp = (Store)p;

                    cmdProperty.CommandText 
                      = @"insert into Stores(surface, commerciality, taxDeclarationId) 
                      OUTPUT INSERTED.ID values(@surface, @commerciality, @taxDeclarationId)";
                    cmdProperty.Parameters.Add("@surface", SqlDbType.Int).Value 
                      = temp.Surface;
                    cmdProperty.Parameters.Add("@commerciality", SqlDbType.Int).Value 
                      = temp.Commerciality;
                    cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
                      = newId;
                    cmdProperty.Prepare();

                    var newPropertyId = (int)cmdProperty.ExecuteScalar();

                    cmdProperty.CommandText 
                      = @"insert into Addresses(street, number, zip, city, propertyId) 
                      values(@street, @number, @zip, @city, @propertyId)";
                    cmdProperty.Parameters.Add("@street", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Street;
                    cmdProperty.Parameters.Add("@number", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.No;
                    cmdProperty.Parameters.Add("@zip", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Zip;
                    cmdProperty.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value
                      = temp.Address.City;
                    cmdProperty.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                      = newPropertyId;
                    cmdProperty.Prepare();
                    cmdProperty.ExecuteNonQuery();
                  }
                  if (p is Plot)
                  {
                    Plot temp = (Plot)p;

                    cmdProperty.CommandText 
                      = @"insert into Plots(surface, cultivated, withinCityLimits, taxDeclarationId) 
                      OUTPUT INSERTED.ID values(@surface, @cultivated, @withinCityLimits, @taxDeclarationId)";
                    cmdProperty.Parameters.Add("@surface", SqlDbType.Int).Value 
                      = temp.Surface;
                    cmdProperty.Parameters.Add("@cultivated", SqlDbType.Bit).Value 
                      = temp.Cultivated;
                    cmdProperty.Parameters.Add("@withinCityLimits", SqlDbType.Bit).Value 
                      = temp.WithinCityLimits;
                    cmdProperty.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value
                      = newId;
                    cmdProperty.Prepare();

                    var newPropertyId = (int)cmdProperty.ExecuteScalar();

                    cmdProperty.CommandText 
                      = @"insert into Addresses(street, number, zip, city, propertyId) 
                      values(@street, @number, @zip, @city, @propertyId)";
                    cmdProperty.Parameters.Add("@street", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Street;
                    cmdProperty.Parameters.Add("@number", SqlDbType.NVarChar, 50).Value
                      = temp.Address.No;
                    cmdProperty.Parameters.Add("@zip", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.Zip;
                    cmdProperty.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value 
                      = temp.Address.City;
                    cmdProperty.Parameters.Add("@propertyId", SqlDbType.Int).Value 
                      = newPropertyId;
                    cmdProperty.Prepare();
                    cmdProperty.ExecuteNonQuery();
                  }
                }
              }
            }
          }
          transaction.Commit();
        }
        catch (SqlException e)
        {
          transaction.Rollback();
          Console.Write("Database error. Exiting..");
          Console.Write(e.StackTrace);
          this.Close();
          System.Environment.Exit(1);
        }
      }
    }

    public void RemoveTaxDeclaration(TaxDeclaration td)
    {
      using (var transaction = Connection.BeginTransaction())
      {
        try
        {
          using (var cmd = new SqlCommand())
          {
            cmd.Connection = Connection;
            cmd.Transaction = transaction;

            cmd.CommandText = @"delete from Addresses 
              where propertyId in (select id from Apartments 
                where taxDeclarationId = @taxDeclarationId)";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from Apartments 
              where taxDeclarationId = @taxDeclarationId";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from Addresses 
              where propertyId in (select id from Stores 
                where taxDeclarationId = @taxDeclarationId)";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from Stores 
              where taxDeclarationId = @taxDeclarationId";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from Addresses 
              where propertyId in (select id from Plots 
                where taxDeclarationId = @taxDeclarationId)";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from Plots 
              where taxDeclarationId = @taxDeclarationId";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"delete from TaxDeclarations 
              where id = @taxDeclarationId";
            cmd.Parameters.Add("@taxDeclarationId", SqlDbType.Int).Value 
              = td.Id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
          }
          transaction.Commit();
        }
        catch (SqlException e)
        {
          transaction.Rollback();
          Console.Write("Database error. Exiting..");
          Console.Write(e.StackTrace);
          this.Close();
          System.Environment.Exit(1);
        }
      }
    }

    private void Close()
    {
      try
      {
        if (Connection != null)
        {
          Connection.Close();
        }
      }
      catch (SqlException e)
      {
        Console.Write("Database error. Exiting..");
        Console.Write(e.StackTrace);
        System.Environment.Exit(1);
      }
    }
  }
}
