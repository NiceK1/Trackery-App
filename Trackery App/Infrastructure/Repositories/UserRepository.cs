using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;
using Trackery_App.Models;

namespace Trackery_App.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        async public Task<bool> AuthenticateUserAsync(NetworkCredential credential)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Password = @Password";
                    command.Parameters.AddWithValue("@Username", credential.UserName);
                    command.Parameters.AddWithValue("@Password", credential.Password);
                    var result = await command.ExecuteScalarAsync();
                    return (int)result > 0;
                }
            }
        }

        public bool RegisterUser(UserModel user)
        {
            // Implementation for user registration
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserModel user)
        {
            // Implementation for updating user details
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            // Implementation for deleting a user
            throw new NotImplementedException();
        }

        public UserModel GetUserById(int userId)
        {
            // Implementation for getting a user by ID
            throw new NotImplementedException();
        }

        public UserModel GetUserByUsername(string username)
        {
            // Implementation for getting a user by username
            throw new NotImplementedException();
        }

        public List<UserModel> GetAllUsers()
        {
            // Implementation for getting all users
            throw new NotImplementedException();
        }
    }
}
