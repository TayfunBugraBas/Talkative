using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Interfaces;

namespace Talkative.Source.ViewModels
{
    public class GroupCreatePageViewModel : BaseviewModel
    {
        private IPageDialogService _pageDialogService;
        private IUser _UserService;
        private IGroup _GroupService;
        
        public GroupCreatePageViewModel(INavigationService navigationService,IPageDialogService pageDialogService,IUser userService, IGroup groupService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _UserService = userService;
            _GroupService = groupService;

        }

        /*End of Constructor*/








    }
}
