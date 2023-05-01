using Android.SE.Omapi;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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
    public class WordsCreatePageViewModel : BaseviewModel
    {
        private IPageDialogService _pageDialogService;
        private IWord _WordService;
        ImageService _ImageService = new ImageService();
        public WordsCreatePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IWord groupService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _WordService = groupService;

        }

        private string wordName;
        public string WordName
        {
            get
            {

                return wordName;
            }
            set
            {

                wordName = value; RaisePropertyChanged();
            }

        }

        private string imgName;
        public string ImageName
        {
            get
            {
                return imgName;
            }
            set
            {
                imgName = value; RaisePropertyChanged();
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

        public bool WordCredentialsValidator
        {
            get
            {
                if (String.IsNullOrWhiteSpace(wordName) == false)
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
        private async void getImage()
        {


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
            else
            {

                await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_FAIL, Constants.Messages.MSG_HEADER_OK);

            }

        }

        public ICommand SelectImage
        {
            get
            {

                return new Command(() =>
                {
                    getImage();

                });


            }

        }

        public ICommand AddWord
        {
            get
            {

                return new Command(async () =>
                {
                    if (ImageCredantialsValidator && WordCredentialsValidator)
                    {
                        var isOk = await _WordService.AddWordAsync(new WordModel
                        {

                            Id = Guid.NewGuid().ToString(),
                            WordGroupId = Models.ActiveGroup.Active_Group.GroupID,
                            Word = WordName,
                            wordIMG = imgNameHelper

                        });
                        if (isOk)
                        {

                            await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_OK, Constants.Messages.MSG_SUCCESS, Constants.Messages.MSG_HEADER_OK);
                            await _navigationService.GoBackAsync();

                        }
                        else
                        {

                            await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_FAIL, Constants.Messages.MSG_HEADER_OK);

                        }
                    }
                    else
                    {

                        await _pageDialogService.DisplayAlertAsync(Constants.Messages.MSG_HEADER_WRONG, Constants.Messages.MSG_EMPTY_ENTRY, Constants.Messages.MSG_HEADER_OK);

                    }
                });


            }

        }

    }
}
