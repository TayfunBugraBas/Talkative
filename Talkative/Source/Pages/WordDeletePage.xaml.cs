using Prism.Navigation;

namespace Talkative.Source.Pages;

public partial class WordDeletePage : ContentPage
{
    private readonly INavigationService _navigationService;
    public WordDeletePage(INavigationService navigation)
    {
        InitializeComponent();
        _navigationService = navigation;
    }
    protected  override bool OnBackButtonPressed()
    {

        
        _navigationService.GoBackAsync();
        _navigationService.NavigateAsync(nameof(WordsPage));
        _navigationService.GoBackAsync();
        _navigationService.NavigateAsync(nameof(WordsPage));

        return true;

    }
}