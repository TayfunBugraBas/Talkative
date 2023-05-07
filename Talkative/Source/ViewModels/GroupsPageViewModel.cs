using DocumentFormat.OpenXml.Spreadsheet;
using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private IWord _WordService;
 

        public GroupsPageViewModel(INavigationService service,IUser user, IPageDialogService pageDialogService, IGroup groupService, IWord wordService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
            _GroupService = groupService;
            _WordService = wordService;
         
        }
      

        /*End of Constructor*/
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

        private ObservableCollection<GroupModel> GetGroups2 = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> group2
        {
            get
            {
                return GetGroups2;
            }
            set
            {
                GetGroups2 = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<WordModel> GetSelectedWords = new ObservableCollection<WordModel>();

        public ObservableCollection<WordModel> wordSelected
        {
            get
            {
                return GetSelectedWords;
            }
            set
            {
                GetSelectedWords = value;
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
        public GroupModel GroupSlector
        {

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

            Refresh();
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in  AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;
            wordSelected = Models.ActiveWords.TalkingWords;

        }

       
        private async  void Refresh()
        {
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;
            wordSelected = Models.ActiveWords.TalkingWords;
        }
        
        public async  void OnDisappearing()
        {
            var objList = new ObservableCollection<GroupModel>();
            GetGroups.Clear();
            var AllGroupsForUser = await _GroupService.GetGroupsByUserID(Models.ActiveUser.CurUser.ID);
            foreach (var item in AllGroupsForUser)
            {
                objList.Add(item);
            }
            group = objList;
            wordSelected = Models.ActiveWords.TalkingWords;
        }

        public ICommand RefreshCommand {

            get {

                return new Command( async () =>
                {
                    if(IsRefresh == true) { 
                        
                         Refresh();
                         IsRefresh = false;
                    
                    }


                });
            }
        
        }
        public  ICommand GadDetailCommand {
            
            get {
               
                return new  Command(  async (object Item) => {
                   
                        var z = Item as GroupModel;
                        Models.ActiveGroup.Active_Group = z;

                        await _Navservice.NavigateAsync(nameof(WordsPage));
                    
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
        public ICommand GoGroupDeletePage
        {

            get
            {

                return new Command(async () =>
                {
                    await _Navservice.NavigateAsync(nameof(GroupDeletePage));


                });
            }

        }
        public ICommand GetSoundInList
        {
            get
            {

                return new Command(async () => {
                    ObservableCollection<WordModel> wordModels = new ObservableCollection<WordModel>();
                    wordModels = Models.ActiveWords.TalkingWords;
                    var locales = await TextToSpeech.GetLocalesAsync();
                    try
                    {
                        var options = new SpeechOptions
                        {
                            Locale = locales.Single(l => l.Country == "TR" || l.Country == "TUR" || l.Country == "tr" || l.Country == "Tr" || l.Country == "Tur" || l.Country == "tr_TR" || l.Country == "tr-TR")
                        };


                        foreach (var item in wordModels)
                        {
                            await TextToSpeech.SpeakAsync(item.Word, options);
                        }
                    }
                    catch
                    {
                        await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_PHONE_LANG_DOES_NOT_SUPPORT, Constants.Messages.MSG_HEADER_OK);

                    }


                });

            }


        }

        public ICommand DeleteLastAdded
        {
            get
            {

                return new Command(async () => {

                    _WordService.removeFromListLastWord();

                });

            }


        }





    }
}
