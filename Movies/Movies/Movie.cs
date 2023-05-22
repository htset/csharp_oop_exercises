namespace Movies
{
  public class Movie
  {
    private int _year;
    private int _duration;

    public string Name { get; set; }
    public int Year
    {
      get => _year;
      set {
        if (value > 1900) 
          _year = value;
        else
          Console.WriteLine("invalid year value");
      }
    }
    public int Duration
    {
      get => _duration;
      set {
        if (value >= 0) 
          _duration = value;
        else
          Console.WriteLine("invalid duration value");
      }
    }
    public string Director { get; set; }
    public List<int> Reviews { get; set; }

    public Movie()
    {
      Name = "N/A";
      Year = DateTime.Now.Year;
      Duration = 0;
      Director = "N/A";
      Reviews = new List<int>();
    }

    public Movie(string name, int year, int duration, String director)
    {
      Name = name;
      Year = year;
      Duration = duration;
      Director = director;
      Reviews = new List<int>();
    }

    public Movie(Movie movie)
    {
      Name = movie.Name;
      Year = movie.Year;
      Duration = movie.Duration;
      Director = movie.Director;
      Reviews = new List<int>(movie.Reviews);
    }

    public void AddReview(int score)
    {
      if (score >= 0 && score <= 5)
        Reviews.Add(score);
      else
        Console.WriteLine("invalid review score");
    }

    public decimal GetReviewAverage()
    {
      decimal sum = 0.0M;
      for (int i = 0; i < Reviews.Count(); i++)
        sum += Reviews.ElementAt(i);

      if (Reviews.Count() > 0)
        return decimal.Round(sum / Reviews.Count(), 1);
      else
        return 0;
    }

    public int GetAge()
    {
      int current_year = DateTime.Now.Year;
      return current_year - Year;
    }

    public override string ToString()
    {
      string reviews = "";
      foreach (var r in Reviews)
        reviews = reviews + r.ToString() + ",";

      if (reviews.Length > 0)
        reviews = reviews.Substring(0, reviews.Length - 1);

      return "Movie{" +
          "name='" + Name + '\'' +
          ", year=" + Year +
          ", duration=" + Duration +
          ", director='" + Director + '\'' +
          ", reviews=" + reviews +
          ", reviews average=" + GetReviewAverage() +
          '}';
    }
  }
}