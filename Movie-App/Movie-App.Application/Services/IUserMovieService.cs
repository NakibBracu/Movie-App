using Movie_App.Domain.Entities;
using Movie_App.Domain.Interfaces;

namespace Movie_App.Application.Services
{
    public interface IUserMovieService
    {
        void AddFavoriteMovie(User user, IMovie movie);
        void RemoveFavoriteMovie(User user, IMovie movie);
        IEnumerable<IMovie> GetFavoriteMovies(User user);
        IEnumerable<IMovie> SearchMovies(User user, string keyword);

    }
}