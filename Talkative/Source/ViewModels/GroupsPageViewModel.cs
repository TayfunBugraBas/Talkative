using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;

namespace Talkative.Source.ViewModels
{
    public class GroupsPageViewModel : BaseviewModel
    {
            
        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;

        public GroupsPageViewModel(INavigationService service,IUser user,IPageDialogService pageDialogService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
        }


        /*End of Constructor*/

        public void test()
        {
            GroupModel gg = new GroupModel();
            gg.GroupID = "xxx";
            gg.groupIMGName = "xxx";
            gg.groupUserID = "xxx";
            gg.groupName = "test";

            GetGroups.Add(gg);


        }

        private  ObservableCollection<GroupModel> GetGroups = new ObservableCollection<GroupModel> { 
        
         new GroupModel {GroupID="xxx",groupIMGName="xx",groupName="xx",groupUserID = "xx"},
         new GroupModel {GroupID="xxx",groupIMGName="xx",groupName="xx",groupUserID = "xx"},
         new GroupModel {GroupID="xxx",groupIMGName="xx",groupName="xx",groupUserID = "xx"}





        };

        public ObservableCollection<GroupModel> gg
        {
            get
            {
                return GetGroups;
            }
            set
            {
                GetGroups = value;
                RaisePropertyChanged();
            }
        }
















    }
}
