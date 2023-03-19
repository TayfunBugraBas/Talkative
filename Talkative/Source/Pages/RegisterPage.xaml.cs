using Talkative.Source.ViewModels;

namespace Talkative.Source.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {

		base.OnBackButtonPressed();
		return true;

    }
}