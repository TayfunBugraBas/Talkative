using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Models;

namespace Talkative.Source.Interfaces
{
    public interface IWord
    {
        public Task<IList<WordModel>> GetWordsByGroupID(string groupID);
        public Task<bool> DeleteWordAsync(string wordId, string GroupId);
        public Task<bool> AddWordAsync(WordModel wordMd);
        Task<List<WordModel>> GetAllWords();

        public bool addToList(WordModel wordMd);
        public bool removeFromListLastWord();
    }
}
