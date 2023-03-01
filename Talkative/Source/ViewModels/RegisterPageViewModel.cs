
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{
    public class RegisterPageViewModel:BaseviewModel
    {
        private INavigationService _Navservice;

        public RegisterPageViewModel(INavigationService service) : base(service)
        {

            _Navservice = service;
          
        }

       public ICommand goBack {
            get {


                return new Command(async () =>
                {

                    await _Navservice.GoBackAsync();

                });


            }
        }

    }
}
