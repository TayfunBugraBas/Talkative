using Android.Media;
using DocumentFormat.OpenXml.Wordprocessing;
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
using Talkative.Source.Services;



namespace Talkative.Source.ViewModels
{
    public  class WordsPageViewModel : BaseviewModel, IPageLifecycleAware
    {

        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IWord _WordService;
     
     


        public WordsPageViewModel(INavigationService service, IUser user, IPageDialogService pageDialogService, IWord wordService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
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


        private WordModel _selectedWord;
        public WordModel WordSlector
        {

            get
            {
                return _selectedWord;
            }
            set
            {
                _selectedWord = value; RaisePropertyChanged();
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
        private string GMansName ;

        public string GroupPageName
        {
            get
            {
                return GMansName;
            }
            set
            {
                GMansName = value; RaisePropertyChanged();
            }
        }



        /*END OF VARIABLES*/
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
            GMansName = Models.ActiveGroup.Active_Group.groupName;
        }

        public void OnDisappearing()
        {
            GetWords.Clear();
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
        
        public ICommand GoWordCreatePage
        {

            get
            {

                return new Command(async () => {

                    await _Navservice.NavigateAsync(nameof(WordsCreatePage));
                
                });


            }


        }
        public ICommand GetWordsForSoundAndList
        {
            get
            {

                return new Command(async (object Item) => {

                    var item = Item as WordModel;
                    var locales  = await TextToSpeech.GetLocalesAsync();
                    var options = new SpeechOptions
                    {
                        Locale = locales.Single(l=>l.Country == "TR")
                    };

                   await  TextToSpeech.SpeakAsync(item.Word, options);
                
   
                });

            }


        }
       
    }
}
