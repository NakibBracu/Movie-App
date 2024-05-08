using Movie_App.Application.Services;
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

    }
}