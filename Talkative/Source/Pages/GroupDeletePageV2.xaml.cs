using Prism.Navigation;

namespace Talkative.Source.Pages;

public partial class GroupDeletePageV2 : ContentPage
{
    private readonly INavigationService _navigationService;
    public GroupDeletePageV2(INavigationService navigation)
    {
		InitializeComponent();
        _navigationService = navigation;
    }

    protected override bool OnBackButtonPressed()
    {
        
        base.OnBackButtonPressed();
        _navigationService.GoBackAsync();
        _navigationService.NavigateAsync(nameof(GroupsPage));
        
        return true;
    }
}