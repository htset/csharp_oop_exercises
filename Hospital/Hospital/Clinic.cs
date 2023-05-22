namespace Hospital
{
  public class Clinic
  {
    public string Name { get; set; }
    public string Director { get; set; }

    public Clinic(string name, string director)
    {
      Name = name;
      Director = director;
    }

    public Clinic()
    {
      Name = "N/A";
      Director = "N/A";
    }
    public override string ToString()
    {
      return "Clinic{" +
          "name='" + Name + '\'' +
          ", director='" + Director + '\'' +
          '}';
    }
  }
}
