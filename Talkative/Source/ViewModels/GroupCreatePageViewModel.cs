using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Talkative.Source.Interfaces;
using Talkative.Source.Services;
using Talkative.Source.Models;
using Talkative.Source.Pages;

namespace Talkative.Source.ViewModels
{

    public class GroupCreatePageViewModel : BaseviewModel
    {
        private IPageDialogService _pageDialogService;
        private IUser _UserService;
        private IGroup _GroupService;
        ImageService _ImageService = new ImageService();

        public GroupCreatePageViewModel(INavigationService navigationService,IPageDialogService pageDialogService,IUser userService, IGroup groupService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _UserService = userService;
            _GroupService = groupService;

        }

        /*End of Constructor*/


        private string groupName;
        public string GroupName
        {
            get { 
            
            return groupName;
            }
            set { 
            
             groupName = value;RaisePropertyChanged();
            }
        
        }

        private string imgName;
        public string ImageName
        {
            get {
                return imgName;
            }
            set { 
            imgName = value;RaisePropertyChanged(); 
            }

        }


        private string imgNameHelper;
        public string ImageNameHelper
        {
            get
            {
                return imgNameHelper;
            }
            set
            {
                imgNameHelper = value; RaisePropertyChanged();
            }

        }

        public bool GroupCredentialsValidator
        {
            get
            {
                if (String.IsNullOrWhiteSpace(groupName) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ImageCredantialsValidator
        {
            get
            {
                if (String.IsNullOrWhiteSpace(imgNameHelper) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }






 
        private ImageSource _selectedImage;

        public ImageSource SelectedImage
        {
            get { return _selectedImage; }
            set { SetProperty(ref _selectedImage, value); }
        }
        private async void getImage() {

            
            var image = await _ImageService.OpenMediaPickerAsync();
            if (image != null)
            {
                var check = Path.Combine(FileSystem.AppDataDirectory, "Images");
                if (!Directory.Exists(check))
                {
                    Directory.CreateDirectory(check);
                }

                var targetPath = Path.Combine(FileSystem.AppDataDirectory, "Images", Path.GetFileName(image.FullPath));

                if (!File.Exists(targetPath))
                {
                    File.Copy(image.FullPath, targetPath);
                }          
                var imageFile = await _ImageService.Upload(image);
                ImageNameHelper = targetPath;

                SelectedImage = ImageSource.FromFile(targetPath);

            }
            else {

                await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_FAIL, Constants.Messages.MSG_HEADER_OK);
            
            }
           
        }

        public ICommand SelectImage
        {
            get {

                return new Command( () =>
                {
                    getImage();

                });
            
            
            }
        
        }

        public ICommand AddGroup
        {
            get
            {

                return new Command(async () =>
                {
                    if (ImageCredantialsValidator && GroupCredentialsValidator)
                    {
                        var isOk = await _GroupService.AddGroupAsync(new GroupModel
                        {

                            GroupID = Guid.NewGuid().ToString(),
                            groupUserID = Models.ActiveUser.CurUser.ID,
                            groupName = groupName,
                            groupIMGName = imgNameHelper

                        });
                        if (isOk)
                        {

                            await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_OK, Constants.Messages.MSG_SUCCESS, Constants.Messages.MSG_HEADER_OK);
                            await _navigationService.NavigateAsync(nameof(MainPage));

                        }
                        else {

                            await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_FAIL, Constants.Messages.MSG_HEADER_OK);

                        }
                    }
                    else {

                        await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_EMPTY_ENTRY, Constants.Messages.MSG_HEADER_OK);

                    }
                });


            }

        }



    }
}
