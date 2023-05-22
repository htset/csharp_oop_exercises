namespace Reservations
{
  public class Reservation
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public decimal Cost { get; set; }
    public List<Person> Persons { get; set; }

    public Reservation(int id, string name, string surname,
                       DateTime startDate, int duration, decimal cost)
    {
      Id = id;
      Name = name;
      Surname = surname;
      StartDate = startDate;
      Duration = duration;
      Cost = cost;
      Persons = new List<Person>();
    }

    public override string ToString()
    {
      string persons = "";
      foreach(var p in Persons)
      {
        persons += p.ToString();
      }

      return "--Reservation--" +
          "\nid=" + Id +
          "\nname=" + Name +
          "\nsurname=" + Surname +
          "\nstartDate=" + StartDate +
          "\nduration=" + Duration +
          "\ncost=" + Cost +
          "\npersons=\n" + persons;
    }

    public void AddPerson(Person p)
    {
      Persons.Add(p);
    }
  }
}
