namespace blazor_tailwind_netflix.Client.Services;

public interface IAuthenticationService
{
    event EventHandler LoginStatusChanged;
    bool IsLoggedIn { get; }
    void Login();
    void Logout();
}

public class AuthneticationService : IAuthenticationService
{
    public bool IsLoggedIn { get;set; }

    public event EventHandler? LoginStatusChanged;

    public void Login()
    {
        IsLoggedIn = true;

        LoginStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Logout()
    {
        IsLoggedIn = false;

        LoginStatusChanged?.Invoke(this, EventArgs.Empty);
    }
}