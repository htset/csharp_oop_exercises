namespace Hospital
{
  public class Measurement
  {
    public double Temp { get; set; }
    public DateTime Date { get; set; }

    public Measurement(double temp, DateTime date)
    {
      Temp = temp;
      Date = date;
    }

    public Measurement()
    {
      Temp = 0;
      Date = DateTime.Now;
    }

    public override string ToString()
    {
      return "Measurement{" +
          "temp=" + Temp +
          ", date=" + Date +
          '}';
    }
  }
}
