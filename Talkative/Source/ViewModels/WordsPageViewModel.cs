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
    public  class WordsPageViewModel : BaseviewModel, IPageLifecycleAware
    {

        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IGroup _GroupService;


        public WordsPageViewModel(INavigationService service, IUser user, IPageDialogService pageDialogService, IGroup groupService) : base(service)
        {
            _userService = user;
            _Navservice = service;
            _PageDialogService = pageDialogService;
            _GroupService = groupService;

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

        /*END OF VARIABLES*/
        public void OnAppearing()
        {
            throw new NotImplementedException();
        }

        public void OnDisappearing()
        {
            throw new NotImplementedException();
        }


    }
}
