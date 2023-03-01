
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{
    public class LoginPageViewModel:BaseviewModel
    {
        private INavigationService _Navservice;

        public LoginPageViewModel(INavigationService service) : base(service)
        {


            _Navservice = service;
        }

        public ICommand RegisterPageCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await _Navservice.NavigateAsync(nameof(RegisterPage));

                });
            }
        }


    }
}
