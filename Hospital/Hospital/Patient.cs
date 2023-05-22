namespace Hospital
{
  public class Patient
  {
    public string Name { get; set; }
    public string Surname { get; set; }
    public int YearOfBirth { get; set; }
    public Clinic? Clinic { get; set; }
    public string Room { get; set; }
    public List<Measurement> Measurements { get; set; }

    public Patient(string name, string surname, int yearOfBirth, Clinic clinic, string room)
    {
      Name = name;
      Surname = surname;
      YearOfBirth = yearOfBirth;
      Clinic = clinic;
      Room = room;
      Measurements = new List<Measurement>();
    }

    public Patient()
    {
      Name = "N/A";
      Surname = "N/A";
      YearOfBirth = 0;
      Clinic = null;
      Room = "N/A";
      Measurements = new List<Measurement>();
    }

    public void insertMeasurement(Measurement m)
    {
      Measurements.Add(m);
    }

    public void insertMeasurement(double temp, DateTime date)
    {
      var m = new Measurement(temp, date);
      Measurements.Add(m);
    }

    public double maxTemp()
    {
      double maxtemp = 0.0;
      foreach (var m in Measurements)
      {
        if (m.Temp > maxtemp)
          maxtemp = m.Temp;
      }

      return maxtemp;
    }

    public override string ToString()
    {
      var measurements = "";
      foreach (var m in Measurements)
        measurements = measurements + m.ToString() + ",";

      if (measurements.Length > 0)
        measurements = measurements.Substring(0, measurements.Length - 1);

      return "Patient{" +
          "name='" + Name + '\'' +
          ", surname='" + Surname + '\'' +
          ", yearOfBirth=" + YearOfBirth +
          ", clinic=" + Clinic +
          ", room='" + Room + '\'' +
          ", pm=[" + measurements +
          "]}";
    }

    public int GetAge()
    {
      return DateTime.Now.Year - YearOfBirth;
    }
  }
}
