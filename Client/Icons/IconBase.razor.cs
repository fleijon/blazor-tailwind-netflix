using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Icons;

public abstract partial class IconBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? InputAttributes { get; set; }
    [Parameter]
    public string Fill { get;set; } = "#FFFFFF";

    public abstract string SVGCode { get; }
}