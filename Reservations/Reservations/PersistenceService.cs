using System.Data;
using System.Data.SqlClient;

namespace Reservations
{
  public class PersistenceService
  {
    SqlConnection Connection;
    public PersistenceService()
    {
      try
      {
        string connectionString;
        connectionString = @"Server=localhost\SQLEXPRESS;Database=Reservations;Trusted_Connection=True;MultipleActiveResultSets=true;";
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

    public List<Apartment> GetApartments()
    {
      var result = new List<Apartment>();
      try
      {
        using (var cmd = new SqlCommand())
        {
          cmd.Connection = Connection;
          cmd.CommandText = "SELECT * from Apartments";
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              result.Add(new Apartment(reader.GetInt32(reader.GetOrdinal("id")),
                  reader.GetString(reader.GetOrdinal("address")),
                  reader.GetInt32(reader.GetOrdinal("capacity")),
                  reader.GetDecimal(reader.GetOrdinal("price")))
              );
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

    public List<Reservation> GetAllReservations()
    {
      var result = new List<Reservation>();
      try
      {
        using (var cmd = new SqlCommand())
        {
          cmd.Connection = Connection;
          cmd.CommandText = "SELECT * from Reservations";
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              var id = reader.GetInt32(reader.GetOrdinal("id"));

              Reservation resv = new Reservation(reader.GetInt32(reader.GetOrdinal("id")),
                  reader.GetString(reader.GetOrdinal("name")),
                  reader.GetString(reader.GetOrdinal("surname")),
                  reader.GetDateTime(reader.GetOrdinal("start_date")),
                  reader.GetInt32(reader.GetOrdinal("duration")),
                  reader.GetDecimal(reader.GetOrdinal("cost")));

              using (var cmdPerson = new SqlCommand())
              {
                cmdPerson.Connection = Connection;
                cmdPerson.CommandText = "SELECT * from Persons where reservation_id=@id";
                cmdPerson.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdPerson.Prepare();

                using (SqlDataReader readerPerson = cmdPerson.ExecuteReader())
                {
                  while (readerPerson.Read())
                  {
                    resv.AddPerson(new Person(readerPerson.GetInt32(reader.GetOrdinal("id")),
                        readerPerson.GetString(readerPerson.GetOrdinal("name")),
                        readerPerson.GetString(readerPerson.GetOrdinal("surname")),
                        readerPerson.GetInt32(readerPerson.GetOrdinal("birth_year")))
                    );
                  }
                }
              }
              result.Add(resv);
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

    public List<Reservation> GetReservationsBySurname(string surname)
    {
      var result = new List<Reservation>();
      try
      {
        using (var cmd = new SqlCommand())
        {
          cmd.Connection = Connection;
          cmd.CommandText = @"SELECT * from Reservations where surname like @surname";
          cmd.Parameters.Add("@surname", SqlDbType.NVarChar, 45).Value = "%" + surname + "%";
          cmd.Prepare();

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              Reservation resv = new Reservation(reader.GetInt32(reader.GetOrdinal("id")),
                  reader.GetString(reader.GetOrdinal("name")),
                  reader.GetString(reader.GetOrdinal("surname")),
                  reader.GetDateTime(reader.GetOrdinal("start_date")),
                  reader.GetInt32(reader.GetOrdinal("duration")),
                  reader.GetDecimal(reader.GetOrdinal("cost")));

              result.Add(resv);
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

    public void InsertReservation(Reservation resv)
    {
      using (var transaction = Connection.BeginTransaction())
      {
        try
        {
          using (var cmd = new SqlCommand())
          {
            cmd.Connection = Connection;
            cmd.Transaction = transaction;
            cmd.CommandText = @"insert into Reservations(name, surname, start_date, duration, cost) 
              OUTPUT INSERTED.ID values(@name, @surname, @start_date, @duration, @cost)";
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 45).Value = resv.Name;
            cmd.Parameters.Add("@surname", SqlDbType.NVarChar, 45).Value = resv.Surname;
            cmd.Parameters.Add("@start_date", SqlDbType.DateTime).Value = resv.StartDate;
            cmd.Parameters.Add("@duration", SqlDbType.Int).Value = resv.Duration;
            cmd.Parameters.Add("@cost", SqlDbType.Decimal, 10).Value = resv.Cost;
            cmd.Parameters["@cost"].Precision = 10;
            cmd.Parameters["@cost"].Scale = 2;
            cmd.Prepare();

            var newId = (int)cmd.ExecuteScalar();

            if (newId > 0)
            {
              List<Person> persons = resv.Persons;

              using (var cmdPerson = new SqlCommand())
              {
                cmdPerson.Connection = Connection;
                cmdPerson.Transaction = transaction;

                foreach (var person in persons)
                {
                  cmdPerson.CommandText = @"insert into Persons(name, surname, birth_year, reservation_id) 
                    values(@name, @surname, @birth_year, @reservation_id)";
                  cmdPerson.Parameters.Add("@name", SqlDbType.NVarChar, 45).Value = person.Name;
                  cmdPerson.Parameters.Add("@surname", SqlDbType.NVarChar, 45).Value = person.Surname;
                  cmdPerson.Parameters.Add("@birth_year", SqlDbType.Int).Value = person.BirthYear;
                  cmdPerson.Parameters.Add("@reservation_id", SqlDbType.Int).Value = newId;
                  cmdPerson.Prepare();
                  cmdPerson.ExecuteNonQuery();
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

    private void Close()
    {
      try
      {
        if (Connection != null)
        {
          Connection.Close();
          Connection.Dispose();
        }
      }
      catch (Exception e)
      {
        Console.Write("Error: Could not close connection");
        Console.Write(e.StackTrace);
      }
    }
  }
}
