using Movie_App.Domain.Interfaces;

namespace Movie_App.Domain.Entities
{
    public class User : IUser
    {
        private readonly string email;
        private readonly List<IMovie> favoriteMovies;

        public User(string email)
        {
            this.email = email;
            this.favoriteMovies = new List<IMovie>();
        }

        public string Email => email;
        public List<IMovie> FavoriteMovies => favoriteMovies;

    }
}