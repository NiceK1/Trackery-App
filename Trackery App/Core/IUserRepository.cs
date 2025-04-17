using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Models;

namespace Trackery_App.Core
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUserAsync(NetworkCredential credential);
        bool RegisterUser(UserModel user);
        bool UpdateUser(UserModel user);
        bool DeleteUser(int userId);
        UserModel GetUserByUsername(string username);
        List<UserModel> GetAllUsers();
    }
}
