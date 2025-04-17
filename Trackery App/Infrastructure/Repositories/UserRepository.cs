using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;
using Trackery_App.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
                    command.CommandText = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Password = @Password COLLATE Latin1_General_CS_AS";
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
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                UPDATE [User]
                SET Email = @Email,
                    Status = @Status,
                    [Name] = @FirstName,
                    LastName = @LastName,
                    Role = @Role
                WHERE Username = @Username";

                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Status", user.Status);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Username", user.Username);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
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
            UserModel user = null;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [User] WHERE Username = @Username";
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                Id = reader[0].ToString(),
                                Username = reader.GetString(1),
                                Password = string.Empty,
                                FirstName = reader.GetString(3),
                                LastName = reader.GetString(4),
                                Email = reader.GetString(5),
                                Role = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return user;
        }

        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [User]";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserModel
                            {
                                Id = reader[0].ToString(),
                                Username = reader.GetString(1),
                                Password = string.Empty,
                                FirstName = reader.GetString(3),
                                LastName = reader.GetString(4),
                                Email = reader.GetString(5),
                                Role = reader.GetString(6),
                                Status = reader.GetBoolean(7)
                            };

                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
    }
}
