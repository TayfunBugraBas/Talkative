
using Talkative.Source.Interfaces;
using Talkative.Source.Pages;
using Talkative.Source.Services;
using Talkative.Source.ViewModels;
namespace Talkative;

public static class PlatformInitializer
{
    public static void RegisterTypes(IContainerRegistry containerRegistry)
    {

        containerRegistry.RegisterInstance(typeof(IUser), new UserService());



        containerRegistry.RegisterForNavigation<NavigationPage>();
        containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
        containerRegistry.RegisterForNavigation<GroupsPage,GroupsPageViewModel>();
        containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel> ();
        containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();

    }
}


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UsePrism(prism =>
            {
                prism.RegisterTypes(container =>
                {
         
  
                    PlatformInitializer.RegisterTypes(container);
                }).OnAppStart(nameof(LoginPage)).OnInitialized(container => {

                });

            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        return builder.Build();
    }
}