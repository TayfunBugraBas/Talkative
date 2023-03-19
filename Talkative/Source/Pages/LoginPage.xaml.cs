namespace Talkative.Source.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {

        base.OnBackButtonPressed();
        return true;

    }
}