using System.Speech.Synthesis;
using Talkative.Source;
using Xamarin.Essentials;
using System.Collections.Generic;
using TextToSpeech = Xamarin.Essentials.TextToSpeech;
using SpeechOptions = Xamarin.Essentials.SpeechOptions;

namespace Talkative;

public partial class GroupsPage : ContentPage
{
	
    
	public GroupsPage()
	{
		InitializeComponent();
        
	}
    protected async override void OnAppearing() { 
    
       base.OnAppearing();

    
    }
    
    private async void CounterBtn_Clicked(object sender, EventArgs e)
	{
        var locales = await Xamarin.Essentials.TextToSpeech.GetLocalesAsync();
        var locale = locales.FirstOrDefault();

       await TextToSpeech.SpeakAsync("selam", new SpeechOptions
        {

            Volume = 0.75f,
            Pitch = 1.0f,
            Locale = locale,
           
            

        });
       
    
    }
}

