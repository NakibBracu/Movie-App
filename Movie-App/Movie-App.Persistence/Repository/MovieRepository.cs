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

        public Movie GetMovieByTitle(string title)
        {
            if (moviesByTitle.TryGetValue(title, out Movie movie))
            {
                return movie;
            }
            return null;
        }

        // Additional methods to add, update, delete movies can be implemented here
        public void AddMovie(Movie movie)
        {
            if (!moviesByTitle.ContainsKey(movie.Title))
            {
                moviesByTitle.Add(movie.Title, movie);
            }
            else
            {
                // Handle the case where the movie with the same title already exists
                throw new InvalidOperationException($"Movie with title '{movie.Title}' already exists.");
            }
        }

        public void UpdateMovie(Movie movie)
        {
            if (moviesByTitle.ContainsKey(movie.Title))
            {
                moviesByTitle[movie.Title] = movie;
            }
            else
            {
                // Handle the case where the movie doesn't exist
                throw new KeyNotFoundException($"Movie with title '{movie.Title}' not found.");
            }
        }

        public void DeleteMovie(string title)
        {
            if (moviesByTitle.ContainsKey(title))
            {
                moviesByTitle.Remove(title);
            }
            else
            {
                // Handle the case where the movie doesn't exist
                throw new KeyNotFoundException($"Movie with title '{title}' not found.");
            }
        }

        public Dictionary<string, Movie> GetAllMovies()
        {
            return moviesByTitle;
        }


    }
}
