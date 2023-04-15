using DocumentFormat.OpenXml.Spreadsheet;
using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{
    public class GroupsPageViewModel : BaseviewModel, IPageLifecycleAware
    {
            
        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IGroup _GroupService;
    

        public GroupsPageViewModel(INavigationService service,IUser user, IPageDialogService pageDialogService, IGroup groupService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
            _GroupService = groupService;
           
        }


        /*End of Constructor*/
     

        public ObservableCollection<GroupModel> GetGroups = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> group
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
        private bool _isRefresh;
        public bool IsRefresh
        {
            get
            {
                return _isRefresh;
            }
            set
            {
                _isRefresh = value; RaisePropertyChanged();
            }
        }

        private GroupModel _selectedGroup;
        public GroupModel SelectedGroup {

            get {
            return _selectedGroup;
            }
            set { 
             _selectedGroup = value; RaisePropertyChanged();
            }
        
        }


        /*End of View Variables*/
        /*Start of Commands And Functions*/
        
        
        public async void OnAppearing()
        {
           

            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in  AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;


        }

       
        private async void Refresh()
        {
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await  _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in AllGroupsForUser)
            {
                objList.Add(item);
            }

            GetGroups = objList;
        }

        public void OnDisappearing()
        {
            throw new NotImplementedException();
        }

        public ICommand RefreshCommand {

            get {

                return new Command( () =>
                {
                    if(IsRefresh == true) { 
                        
                         Refresh();
                         IsRefresh = false;
                    
                    }


                });
            }
        
        }
        public ICommand GroupDetailCommand {

            get {

                return new Command(async () => {



                    throw new NotImplementedException();
                
                });
            
            
            }
        
        
        }
        public ICommand GoGroupCreatePage
        {

            get
            {

                return new Command(async () =>
                {
                   await _Navservice.NavigateAsync(nameof(GroupCreatePage));


                });
            }

        }






    }
}
