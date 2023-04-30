using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;

namespace Talkative.Source.Services
{
    public class GroupService: IGroup
    {
        private FirebaseClient FbClient = new FirebaseClient("https://talkative-a62ae-default-rtdb.europe-west1.firebasedatabase.app/");

        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBase.Key.webApiKey.ToString()));
        
        public async Task<bool> AddGroupAsync(GroupModel groupMd)
        {
            try
            {
                await FbClient.Child("Groups").PostAsync(JsonConvert.SerializeObject(groupMd));
                return true;
            }
            catch {

                return false;
            }
        }

        public async Task<bool> DeleteGroupAsync(string GroupId,string UserID)
        {
            try
            {
                var toDelete = (await FbClient.Child("Groups").OnceAsync<GroupModel>()).Where(x => x.Object.groupUserID == UserID && x.Object.GroupID == GroupId).FirstOrDefault();
                await FbClient.Child("Groups").Child(toDelete.Key).DeleteAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

      
      
        public async  Task<GroupModel> findGroupByGroupId(string GID)
        {
            var grouplist = await GetAllGroups();

            GroupModel group = grouplist.Where(x => x.GroupID == GID).FirstOrDefault();

            return group;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            return (await FbClient.Child("Groups").OnceAsync<GroupModel>()).Select(x => new GroupModel
            {

                GroupID = x.Object.GroupID,
                groupName = x.Object.groupName,
                groupIMGName = x.Object.groupIMGName,
                groupUserID = x.Object.groupUserID,
                ImageSource = x.Object.groupIMGName

            }).ToList();
        }

        public async  Task<IList<GroupModel>> GetGroupsByUserID(string userID)
        {
            var grouplist = await GetAllGroups();

            return grouplist.Where(x => x.groupUserID == userID).ToList();


        }
    }
}
