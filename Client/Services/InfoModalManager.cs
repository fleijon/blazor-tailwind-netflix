namespace blazor_tailwind_netflix.Client.Services;

public interface IInfoModalManager
{
    void Open(Guid movieId);
    void Close();

    event EventHandler<Guid> OnOpen;
    event EventHandler OnClosed;
}

public class InfoModalManager : IInfoModalManager
{
    public event EventHandler<Guid>? OnOpen;
    public event EventHandler? OnClosed;
    public void Close()
    {
        OnClosed?.Invoke(this, EventArgs.Empty);
    }

    public void Open(Guid movieId)
    {
        OnOpen?.Invoke(this, movieId);
    }
}
