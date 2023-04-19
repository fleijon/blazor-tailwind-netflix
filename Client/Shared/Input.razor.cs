using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Shared;

public partial class Input
{
    [Parameter]
    public string? Id { get;set; }
    [Parameter]
    public string? Value { get;set; }
    [Parameter]
    public string? Type { get;set; }
    [Parameter]
    public string? Label { get;set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private Task OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();

        return ValueChanged.InvokeAsync(Value);
    }
}
