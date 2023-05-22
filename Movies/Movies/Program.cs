namespace Movies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Movie m1 = new Movie();
            Movie m2 = new Movie("Hunger Games", 2012, 142, "Gary Ross");
            m2.AddReview(5);
            m2.AddReview(4);
            m2.AddReview(6);

            Movie m3 = new Movie(m2);
            m3.AddReview(2);
            Console.WriteLine(m1);
            Console.WriteLine(m2);
            Console.WriteLine(m3);
        }
    }
}
