using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Models;

namespace Talkative.Source.Interfaces
{
    public interface IUser
    {
        Task<bool> UserLogin(string email, string password);
        Task<bool> RegisterUser(UserModel user);
        Task<bool> ChangePassword(string password);
        Task<bool> ForgetPassword(string email);
        Task<UserModel> GetUserById(string Id);
        Task<List<UserModel>> GetAllUser();
        Task<bool> AddUser(UserModel user);
        Task<bool> UpdateUser(UserModel user);

    }
}
