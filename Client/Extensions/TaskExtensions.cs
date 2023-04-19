namespace blazor_tailwind_netflix.Client.Extensions;

public static class TaskExtensions
{
    public static async Task CatchException(this Task target, Action<Exception> onExceptionCallback)
    {
        try
        {
            await target;
        }
        catch (Exception ex)
        {
            onExceptionCallback(ex);
        }
    }
}
