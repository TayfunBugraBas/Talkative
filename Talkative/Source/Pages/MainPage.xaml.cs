namespace Talkative.Source.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {

        base.OnBackButtonPressed();
        return true;

    }
}