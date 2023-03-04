using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;
using Firebase.Auth;
using Firebase.Database;


namespace Talkative.Source.Services
{
    public class UserService : IUser
    {
        private FirebaseClient FbClient = new FirebaseClient("https://talkative-a62ae-default-rtdb.europe-west1.firebasedatabase.app/");

        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBase.Key.webApiKey));

        public UserService()
        {

        }

        public Task<bool> AddUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserById(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
