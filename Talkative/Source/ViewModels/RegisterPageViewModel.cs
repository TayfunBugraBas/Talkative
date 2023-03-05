
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Interfaces;
using Talkative.Source.Models;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{
    public class RegisterPageViewModel:BaseviewModel
    {
        private INavigationService _Navservice;
        private IPageDialogService _PageDialogService;
        private IUser _UserService;

        public RegisterPageViewModel(INavigationService service,IPageDialogService pageDialogService,IUser UserService) : base(service)
        {

            _Navservice = service;
            _PageDialogService = pageDialogService;
            _UserService = UserService;


        }
        /*END OF CONSTRUCTOR*/

        private string email;
        private string password;
        private string rePassword;
        public string PEmail
        {
            get { return email; }

            set { email = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(EmailCredentialsValidator)); }

        }

        public string Ppassword
        {

            get { return password; }

            set { password = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(PasswordCredantialsValidator)); }

        }
        public string Repassword
        {

            get { return rePassword; }

            set { rePassword = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(PasswordCredantialsValidator)); }

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



        public ICommand goBack {
            get {


                return new Command(async () =>
                {

                    await _Navservice.GoBackAsync();

                });


            }
        }

        public ICommand RegisterCommand
        {
            get {

                return new Command(async () =>
                {
                    if (PasswordCredantialsValidator && EmailCredentialsValidator && rePassword == password)
                    {

                        var isOk = await _UserService.RegisterUser(new UserModel { 
                        
                            ID = "default",
                            Email = email,
                            Password = password,
                            Adress = null,
                            PhoneForEmergency = null
       
                        });

                        if (isOk)
                        {
                            await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_SUCCESS, Constants.Messages.VERIFY_EMAIL, Constants.Messages.MSG_HEADER_OK);

                        }
                        else {

                            await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_PASSWORD_OR_EMAIL_WRONG, Constants.Messages.MSG_HEADER_OK);

                        }

                    }
                    else {

                       await _PageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_PASSWORD_OR_EMAIL_WRONG, Constants.Messages.MSG_HEADER_OK);
                    
                    }

                });
            }  
        }

    }
}
