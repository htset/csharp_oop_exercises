namespace Reservations
{
  public class Person
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int BirthYear { get; set; }

    public Person(int id, string name, string surname, int birthYear)
    {
      Id = id;
      Name = name;
      Surname = surname;
      BirthYear = birthYear;
    }

    public override string ToString()
    {
      return "--Person--" +
          "\nid=" + Id +
          "\nname=" + Name +
          "\nsurname=" + Surname +
          "\nbirthYear=" + BirthYear;
    }
  }
}
