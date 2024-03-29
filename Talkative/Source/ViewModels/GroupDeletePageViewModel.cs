﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;

namespace Talkative.Source.ViewModels
{
    public class GroupDeletePageViewModel: BaseviewModel,IPageLifecycleAware
    {

        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IGroup _GroupService;
        public GroupDeletePageViewModel(INavigationService service, IUser user, IPageDialogService pageDialogService, IGroup groupService) :base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
            _GroupService = groupService;
        }
        private GroupModel _selectedGroup;
        public GroupModel GroupSlector
        {

            get
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value; RaisePropertyChanged();
            }
        }

        private ObservableCollection<GroupModel> GetGroups = new ObservableCollection<GroupModel>();

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
        public async void OnAppearing()
        {
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;
        }

        public void OnDisappearing()
        {
            throw new NotImplementedException();
        }

        public ICommand GoBack
        {

            get
            {

                return new Command(async () =>
                {
                    await _Navservice.GoBackAsync();


                });
            }

        }
        private async void Refresh()
        {
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;
            
        }

        public ICommand DeleteGroup
        {
            get
            {
                return new Command(async (object item) => {
                    try
                    {
                        var xitem = item as GroupModel;
                        await _GroupService.DeleteGroupAsync(xitem.GroupID, Models.ActiveUser.CurUser.ID);
                        await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_OK, Constants.Messages.MSG_SUCCESS, Constants.Messages.MSG_HEADER_OK);
                        Refresh();
                    }
                    catch
                    {
                        await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_FAIL, Constants.Messages.MSG_HEADER_OK);

                    }

                });
            }
        }
    }
}
