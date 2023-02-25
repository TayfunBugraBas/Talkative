using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.ViewModels
{
    public class BaseviewModel : BindableBase, IPageLifecycleAware, INavigatedAware, IInitialize
    {
        private INavigationService _navigationService { get; set; }
        public BaseviewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void Initialize(INavigationParameters parameters)
        {
            
        }

        public void OnAppearing()
        {

           
        }

        public void OnDisappearing()
        {
            
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        } // sayfalar arası veri gönderme

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
