using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Models;

namespace Talkative.Source.Interfaces
{
    public interface IGroup
    {
        public Task<IList<GroupModel>> GetGroupsByUserID(string userID);

        public Task<bool> DeleteGroupAsync(string GroupId, string UserID);

        public Task<bool> AddGroupAsync(GroupModel groupMd);





    }
}
