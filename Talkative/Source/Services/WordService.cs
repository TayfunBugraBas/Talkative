using DocumentFormat.OpenXml.Spreadsheet;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;

namespace Talkative.Source.Services
{
    public class WordService : IWord
    {
        private FirebaseClient FbClient = new FirebaseClient("https://talkative-a62ae-default-rtdb.europe-west1.firebasedatabase.app/");

        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBase.Key.webApiKey.ToString()));
        public async Task<bool> AddWordAsync(WordModel wordMd)
        {

            try
            {
                await FbClient.Child("Words").PostAsync(JsonConvert.SerializeObject(wordMd));
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async  Task<bool> DeleteWordAsync(string wordId, string GroupId)
        {
            try
            {
                var toDelete = (await FbClient.Child("Words").OnceAsync<WordModel>()).Where(x => x.Object.Id == wordId && x.Object.WordGroupId == GroupId).FirstOrDefault();
                await FbClient.Child("Words").Child(toDelete.Key).DeleteAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public  async Task<List<WordModel>> GetAllWords()
        {
            return (await FbClient.Child("Words").OnceAsync<WordModel>()).Select(x => new WordModel
            {

                Id = x.Object.Id,
                Word = x.Object.Word,
                WordGroupId = x.Object.WordGroupId,
                wordIMG = x.Object.wordIMG,
                ImageSource = x.Object.wordIMG

            }).ToList();
        }

        public async Task<IList<WordModel>> GetWordsByGroupID(string groupID)
        {
            var grouplist = await GetAllWords();

            return grouplist.Where(x => x.WordGroupId == groupID).ToList();
        }
    }
}
