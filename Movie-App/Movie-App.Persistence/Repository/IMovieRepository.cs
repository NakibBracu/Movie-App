using Movie_App.Domain.Entities;

namespace Movie_App.Persistence.Repository
{
    public interface IMovieRepository
    {
        Movie GetMovieByTitle(string title);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(string title);

        Dictionary<string, Movie> GetAllMovies();
    }
}