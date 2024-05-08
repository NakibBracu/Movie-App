using Movie_App.Domain.Entities;

namespace Movie_App.Persistence.Repository
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string email);
    }
}