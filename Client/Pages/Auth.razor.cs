using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Pages;

public partial class Auth
{
    [Inject]
    public IAuthenticationService AuthService { get;set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get;set; } = null!;
    private string? email;
    private string? username;
    private string? password;
    private string? variant = "login";
    private bool isLogin => !string.IsNullOrEmpty(variant) && variant == "login";

    private void ToggleVariant()
    {
        if(variant == "login")
        {
            variant = "register";
        }
        else
        {
            variant = "login";
        }
    }

    private void LoginOrRegister()
    {
        if(isLogin)
        {
            Authenticate();
        }
        else
        {
            Register();
        }
    }

    private void Register()
    {
        username = null;
        email = null;
        password = null;

        StateHasChanged();
        ToggleVariant();
    }

    private void Authenticate()
    {
        AuthService.Login();

        username = null;
        email = null;
        password = null;
        StateHasChanged();

        NavigationManager.NavigateTo("/profiles");
    }
}