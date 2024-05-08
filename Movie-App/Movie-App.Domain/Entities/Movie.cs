using Movie_App.Domain.Interfaces;

namespace Movie_App.Domain.Entities
{
    public class Movie : IMovie
    {
        public string Title { get; }
        public IEnumerable<string> Cast { get; }
        public string Category { get; }
        public DateTime ReleaseDate { get; }
        public double Budget { get; }

        public Movie(string title, IEnumerable<string> cast, string category, DateTime releaseDate, double budget)
        {
            Title = title;
            Cast = cast.ToList(); // To ensure immutability
            Category = category;
            ReleaseDate = releaseDate;
            Budget = budget;
        }
    }
}