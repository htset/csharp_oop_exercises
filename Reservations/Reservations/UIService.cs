namespace Reservations
{
  public class UIService
  {
    PersistenceService Ps;

    public UIService(PersistenceService ps)
    {
      Ps = ps;
    }

    public void Menu()
    {
      int option;
      do
      {
        Console.WriteLine("Options: ");
        Console.WriteLine("1) Add reservation ");
        Console.WriteLine("2) Search reservation ");
        Console.WriteLine("3) View all reservations ");
        Console.WriteLine("0) Exit ");
        Console.Write("Enter your selection:");
        option = Convert.ToInt32(Console.ReadLine());

        switch (option)
        {
          case 1:
            AddReservation();
            break;
          case 2:
            SearchReservation();
            break;
          case 3:
            ListReservations();
            break;
          case 0:
            break;
          default:
            Console.Write("Please enter selection again:");
            break;
        }
      } while (option != 0);
    }

    private void AddReservation()
    {
      int option;
      string name, surname;
      DateTime startDate;
      int duration;

      Console.Write("Name: ");
      name = Console.ReadLine() ?? "";

      Console.Write("Surname: ");
      surname = Console.ReadLine() ?? "";

      Console.Write("Start date (YYYY-MM-DD): ");
      DateTime.TryParse(Console.ReadLine(), out startDate);

      Console.Write("Duration: ");
      duration = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine("Available apartments:");
      var apartments = Ps.GetApartments();
      int i = 1;
      foreach (var a in apartments)
      {
        Console.WriteLine("Apartment no." + (i++) + ":");
        Console.WriteLine(a);
      }
      Console.Write("(Press 0 to cancel): ");
      option = Convert.ToInt32(Console.ReadLine());

      if (option > 0 && option <= apartments.Count)
      {
        var resv = new Reservation(0, name, surname, startDate, duration,
            apartments.ElementAt(option - 1).Price);
        AddPersons(resv);
        Ps.InsertReservation(resv);
      }
    }

    private void AddPersons(Reservation resv)
    {
      string name, surname, selection;
      int birthYear;

      Console.WriteLine("Give the persons:");
      do
      {
        Console.Write("Name: ");
        name = Console.ReadLine() ?? "";
        Console.Write("Surname: ");
        surname = Console.ReadLine() ?? "";
        Console.Write("Birth Year: ");
        birthYear = Convert.ToInt32(Console.ReadLine());

        resv.AddPerson(new Person(0, name, surname, birthYear));

        Console.Write("Add another person? (y/n): ");
        selection = Console.ReadLine() ?? "";
      }
      while (!selection.Equals("n") && !selection.Equals("N"));
    }

    private void SearchReservation()
    {
      string surname;
      Console.Write("Enter surname (also partial): ");
      surname = Console.ReadLine() ?? "";
      var reservations = Ps.GetReservationsBySurname(surname);

      foreach (var r in reservations)
      {
        Console.WriteLine(r);
      }
    }

    private void ListReservations()
    {
      var reservations = Ps.GetAllReservations();
      foreach (Reservation r in reservations)
      {
        Console.WriteLine(r);
      }
    }
  }

}
