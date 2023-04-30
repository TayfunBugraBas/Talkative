namespace Talkative.Source.Pages;

public partial class WordsCreatePage : ContentPage
{
	public WordsCreatePage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {
        base.OnBackButtonPressed();
        return true;
    }
}