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

        public User GetUserByEmail(string email)
        {
            if (usersByEmail.TryGetValue(email, out User user))
            {
                return user;
            }
            return null;
        }

        // Additional methods to add, update, delete users can be implemented here
        public void AddUser(User user)
        {
            if (!usersByEmail.ContainsKey(user.Email))
            {
                usersByEmail.Add(user.Email, user);
            }
            else
            {
                // Handle the case where the user with the same email already exists
                throw new InvalidOperationException($"User with email '{user.Email}' already exists.");
            }
        }

        // Method to update an existing user
        public void UpdateUser(User user)
        {
            if (usersByEmail.ContainsKey(user.Email))
            {
                usersByEmail[user.Email] = user;
            }
            else
            {
                // Handle the case where the user doesn't exist
                throw new KeyNotFoundException($"User with email '{user.Email}' not found.");
            }
        }

        // Method to delete an existing user
        public void DeleteUser(string email)
        {
            if (usersByEmail.ContainsKey(email))
            {
                usersByEmail.Remove(email);
            }
            else
            {
                // Handle the case where the user doesn't exist
                throw new KeyNotFoundException($"User with email '{email}' not found.");
            }
        }
    }
}