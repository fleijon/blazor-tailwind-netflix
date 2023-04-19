using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace blazor_tailwind_netflix.Client.Shared;

public class AuthorizeView : ComponentBase, IDisposable
{
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;

    [Parameter] public RenderFragment? NotAuthorized { get; set; }

    [Parameter] public RenderFragment? Authorized { get; set; }

    protected override void OnInitialized()
    {
        AuthenticationService.LoginStatusChanged += OnLoginStatusChanged;
    }

    private void OnLoginStatusChanged(object? sender, EventArgs e)
    {
        StateHasChanged();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if(AuthenticationService.IsLoggedIn)
        {
            builder.AddContent(0, Authorized);
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }
    }

    public void Dispose()
    {
        AuthenticationService.LoginStatusChanged -= OnLoginStatusChanged;
    }
}
