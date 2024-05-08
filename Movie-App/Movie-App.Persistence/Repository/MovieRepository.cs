using Movie_App.Domain.Entities;

namespace Movie_App.Persistence.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly Dictionary<string, Movie> moviesByTitle;

        public MovieRepository()
        {
            moviesByTitle = new Dictionary<string, Movie>();
        }


    }
}
