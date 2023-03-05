
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Interfaces;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{
    public class LoginPageViewModel:BaseviewModel
    {
        private IUser _userService;
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;

        public LoginPageViewModel(INavigationService service,IUser userService, IPageDialogService pageDialogService) : base(service)
        {

            _userService = userService;
            _Navservice = service;
            _PageDialogService = pageDialogService;
        }

        /*END OF CONSTRUCTOR*/

        private string email;
        private string password;

        public string PEmail
        {
            get { return email; }

            set { email = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(EmailCredentialsValidator)); }
        
        }

        public string Ppassword { 
          
            get { return password; }

            set { password = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(PasswordCredantialsValidator)); }
        
        }
        /*END OF VIEW VARIABLES*/


        public bool EmailCredentialsValidator
        {
            get
            {
                if (String.IsNullOrWhiteSpace(email) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool PasswordCredantialsValidator
        {
            get
            {
                if (String.IsNullOrWhiteSpace(password) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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

        public ICommand LoginCommand
        {
            get
            {
                
                    return new Command(async () =>
                    {
                        if (EmailCredentialsValidator && PasswordCredantialsValidator)
                        {

                            var isOK = await _userService.UserLogin(email, password);

                            if (isOK)
                            {
                                Models.ActiveUser.CurUser = await _userService.GetUserById(Models.ActiveUser.FbUser.User.LocalId);/*Mustafadan Firebase Kurallarını Al*/


                                if (Models.ActiveUser.FbUser.User.IsEmailVerified == true)
                                {
                                    await _Navservice.NavigateAsync(nameof(MainPage));
                                }
                                else {

                                    await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.VERIFY_EMAIL, Constants.Messages.MSG_HEADER_OK);
                                
                                }
                            }
                            else {

                                await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG,Constants.Messages.MSG_PASSWORD_OR_EMAIL_WRONG,Constants.Messages.MSG_HEADER_OK);
                            }


                        }

                    });
                
            }
        }


    }
}
