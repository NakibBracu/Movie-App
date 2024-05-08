using Movie_App.Application.Services;
using Movie_App.Domain.Entities;
using Movie_App.Domain.Interfaces;
using Movie_App.Persistence.Repository;

namespace Movie_App.Infrastructure.Services
{
    public class UserMovieService : IUserMovieService
    {
        private readonly IUserRepository userRepository;
        private readonly IMovieRepository movieRepository;

        public UserMovieService(IUserRepository userRepository, IMovieRepository movieRepository)
        {
            this.userRepository = userRepository;
            this.movieRepository = movieRepository;
        }

        public void AddFavoriteMovie(User user, IMovie movie)
        {
            // Check if the user exists
            if (userRepository.GetUserByEmail(user.Email) == null)
            {
                // Handle the case where the user doesn't exist
                throw new KeyNotFoundException($"User with email '{user.Email}' not found.");
            }

            // Check if the movie exists
            if (movieRepository.GetMovieByTitle(movie.Title) == null)
            {
                // Handle the case where the movie doesn't exist
                throw new KeyNotFoundException($"Movie with ID '{movie.Title}' not found.");
            }

            // Add the movie to the user's favorites
            user.FavoriteMovies.Add(movie);
        }

        public void RemoveFavoriteMovie(User user, IMovie movie)
        {
            // Check if the user exists
            if (userRepository.GetUserByEmail(user.Email) == null)
            {
                // Handle the case where the user doesn't exist
                throw new KeyNotFoundException($"User with email '{user.Email}' not found.");
            }

            // Remove the movie from the user's favorites
            user.FavoriteMovies.Remove(movie);
        }

        public IEnumerable<IMovie> GetFavoriteMovies(User user)
        {
            // Check if the user exists
            if (userRepository.GetUserByEmail(user.Email) == null)
            {
                // Handle the case where the user doesn't exist
                throw new KeyNotFoundException($"User with email '{user.Email}' not found.");
            }

            // Return the user's favorite movies
            return user.FavoriteMovies;
        }

        public IEnumerable<IMovie> SearchMovies(User user, string keyword)
        {
            var userFavorites = user.FavoriteMovies;
            return userFavorites.Where(movie =>
                movie.Title.ToLower().Contains(keyword.ToLower()) ||
                movie.Cast.Any(actor => actor.ToLower().Contains(keyword.ToLower())) ||
                movie.Category.ToLower().Contains(keyword.ToLower())
            );
        }


    }
}