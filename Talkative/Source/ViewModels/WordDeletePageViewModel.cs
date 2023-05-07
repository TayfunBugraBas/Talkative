using System;
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
    public class WordDeletePageViewModel: BaseviewModel,IPageLifecycleAware
    {

        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IGroup _GroupService;
        private IWord _WordService;
        public WordDeletePageViewModel(INavigationService service, IUser user, IPageDialogService pageDialogService, IGroup groupService, IWord wordService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
            _GroupService = groupService;
            _WordService = wordService;
        }
        public ObservableCollection<WordModel> GetWords = new ObservableCollection<WordModel>();

        public ObservableCollection<WordModel> word
        {
            get
            {
                return GetWords;
            }
            set
            {
                GetWords = value;
                RaisePropertyChanged();
            }
        }
        public async void OnAppearing()
        {
            var objList = new ObservableCollection<WordModel>();
            GetWords.Clear();
            var AllWordsForUser = await _WordService.GetWordsByGroupID(Models.ActiveGroup.Active_Group.GroupID);
            foreach (var item in AllWordsForUser)
            {
                objList.Add(item);
            }
            word = objList;
           
           
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
            var objList = new ObservableCollection<WordModel>();
            GetWords.Clear();
            var AllWordsForUser = await _WordService.GetWordsByGroupID(Models.ActiveGroup.Active_Group.GroupID);
            foreach (var item in AllWordsForUser)
            {
                objList.Add(item);
            }
            word = objList;
        }

        public ICommand DeleteWord
        {
            get
            {
                return new Command(async (object item) => {
                    try
                    {
                        var xitem = item as WordModel;
                        await _WordService.DeleteWordAsync(xitem.Id, Models.ActiveGroup.Active_Group.GroupID);
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
