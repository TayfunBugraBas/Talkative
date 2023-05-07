namespace Talkative.Source.Pages;

public partial class WordsPage : ContentPage
{
	private readonly INavigationService _navigationService;
	public WordsPage(INavigationService navigation)
	{
		InitializeComponent();
		_navigationService = navigation;
	

	}
    protected override bool OnBackButtonPressed()
    {
		_navigationService.NavigateAsync(nameof(GroupsPage));
        base.OnBackButtonPressed();
        return true;
    }


}