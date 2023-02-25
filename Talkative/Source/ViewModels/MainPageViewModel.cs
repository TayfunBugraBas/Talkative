using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TextToSpeech = Xamarin.Essentials.TextToSpeech;
using SpeechOptions = Xamarin.Essentials.SpeechOptions;
using System.Collections.ObjectModel;

namespace Talkative.Source.ViewModels
{
    public class MainPageViewModel:BaseviewModel
    {
        private readonly INavigationService _service;
        private string _talkTest;
        private List<string> _speakers = new List<string>();


        //get set
        public string TalkText { get{ return _talkTest; } set { _talkTest = value; RaisePropertyChanged(); } }
        public List<string> Speakers { get { return _speakers; } set { _speakers = value; RaisePropertyChanged(); } }

        public MainPageViewModel(INavigationService service):base(service) {


            _service = service;



        }
        public void OnAppearing() { 
            base.OnAppearing();
            Speakers.Add("asd");
            Speakers.Add("asd");
            Speakers.Add("asd");

        }
      

        public ICommand SecondPageCommand { get {
                return new Command(async () =>
                {
                    //   await _service.NavigateAsync(nameof(GroupsPage));
                    Speakers.Add("asd");

                });
        }}

    }
}
/*var locales = await Xamarin.Essentials.TextToSpeech.GetLocalesAsync();
var locale = locales.FirstOrDefault();

await TextToSpeech.SpeakAsync("selam", new SpeechOptions
{

    Volume = 0.75f,
    Pitch = 1.0f,
    Locale = locale,



});*/