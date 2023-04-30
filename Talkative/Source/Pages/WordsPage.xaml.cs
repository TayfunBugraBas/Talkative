namespace Talkative.Source.Pages;

public partial class WordsPage : ContentPage
{
	private INavigationService _navigationService;
	public WordsPage(INavigationService navigation)
	{
		InitializeComponent();
		_navigationService = navigation;
	

	}
    protected override bool OnBackButtonPressed()
    {
		_navigationService.GoBackAsync();
        base.OnBackButtonPressed();
        return true;
    }


}