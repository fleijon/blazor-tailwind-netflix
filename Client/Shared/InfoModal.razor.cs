using System.ComponentModel;
using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Shared;

public partial class InfoModal : IDisposable
{
    [Inject]
    public IInfoModalManager InfoModalManager { get;set; } = null!;
    [Inject]
    public MovieStore.IMovies MovieStore { get;set; } = null!;

    private bool isVisible = false;
    private MovieStore.Movie? movie;

    protected override void OnInitialized()
    {
        InfoModalManager.OnOpen += OnOpen;
        InfoModalManager.OnClosed += OnClosed;
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        movie = null;
        isVisible = false;
    }

    private void Close()
    {
        InfoModalManager.Close();
    }

    private void OnOpen(object? sender, Guid e)
    {
        movie = MovieStore.GetMovie(e);
        isVisible = true;
        StateHasChanged();
    }

    public void Dispose()
    {
        InfoModalManager.OnOpen -= OnOpen;
        InfoModalManager.OnClosed -= OnClosed;
    }
}