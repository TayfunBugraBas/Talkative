using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;
using Firebase.Auth;
using Firebase.Database;
using Microsoft.Maui.Layouts;
using Scrypt;
using Newtonsoft.Json;
using Firebase.Database.Query;

namespace Talkative.Source.Services
{
    public class UserService : IUser
    {
        private FirebaseClient FbClient = new FirebaseClient("https://talkative-a62ae-default-rtdb.europe-west1.firebasedatabase.app/");

        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBase.Key.webApiKey.ToString()));

        ScryptEncoder scryptEncoder = new ScryptEncoder();  

        public UserService()
        {


        }

        public async Task<bool> AddUser(UserModel user)
        {
            try
            {
                user.Password = scryptEncoder.Encode(user.Password);
                

                await FbClient.Child("Users").PostAsync(JsonConvert.SerializeObject(user));

                return true;

            }
            catch {
            
               return false;
            
            }
        }

        public async Task<bool> ChangePassword(string password)
        {
            try
            {
                var auth = await authProvider.ChangeUserPassword(Models.ActiveUser.FbUser.FirebaseToken, password);
                var currentUser = Models.ActiveUser.CurUser;
                var encodedPassword = scryptEncoder.Encode(password);
                currentUser.Password = encodedPassword;
                await UpdateUser(currentUser);
                return true;

            }
            catch {

                return false;
            }

        }

        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                await authProvider.SendPasswordResetEmailAsync(email);

                return true;
            }
            catch 
            {
                return false;
            }

        }

        public async Task<List<UserModel>> GetAllUser()
        {
            return (await FbClient.Child("Users").OnceAsync<Models.UserModel>()).Select(x => new UserModel
            {

                ID = x.Object.ID,
                Email = x.Object.Email,
                Password = x.Object.Password,
                Adress = x.Object.Adress,
                PhoneForEmergency = x.Object.PhoneForEmergency
 
            }).ToList();

        }

        public async Task<UserModel> GetUserById(string Id)
        {
            var users = await GetAllUser();

            return users.Where(x => x.ID == Id).FirstOrDefault();
        }

        public async Task<bool> RegisterUser(UserModel user)
        {
            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password);
                var firebaseUserInfo = await authProvider.UpdateProfileAsync(auth.FirebaseToken, user.Email.ToString(), null);
                await authProvider.SendEmailVerificationAsync(auth);

                user.ID = auth.User.LocalId;
                UserModel newUser = user;


                await AddUser(newUser);
                return true;
            }
            catch { 
            
                
                return false;
            }

        }

        public async Task<bool> UpdateUser(UserModel user)
        {
            try
            {
                var toUpdatePerson = (await FbClient
                .Child("Users")
                .OnceAsync<UserModel>()).Where(a => a.Object.ID == user.ID).FirstOrDefault();

                await FbClient
                  .Child("Users")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(user);

                return true;

            }
            catch {

                return false;
            }
        }

        public async Task<bool> UserLogin(string email, string password)
        {
            try {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                Models.ActiveUser.FbUser = auth;
                if (Models.ActiveUser.FbUser != null)
                {

                    return true;

                }
                else {

                    return false;

                }
            
            }
            catch {

                return false;

            }

        }
    }
}
