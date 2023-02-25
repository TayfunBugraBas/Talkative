using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.ViewModels
{
    public class LoginPageViewModel:BaseviewModel
    {
        private INavigationService _service;

        public LoginPageViewModel(INavigationService service) : base(service)
        {


            _service = service;
        }
    }
}
