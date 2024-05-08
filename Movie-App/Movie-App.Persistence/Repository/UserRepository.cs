using Movie_App.Domain.Entities;

namespace Movie_App.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> usersByEmail;

        public UserRepository()
        {
            usersByEmail = new Dictionary<string, User>();
        }

    }
}