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

    private bool componentIsVisible = false;
    private bool isVisible = false;
    private MovieStore.Movie? movie;

    protected override void OnInitialized()
    {
        InfoModalManager.OnOpen += OnOpen;
        InfoModalManager.OnClosed += OnClosed;
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        componentIsVisible = false;
        movie = null;
    }

    private async Task Close()
    {
        isVisible = false;
        await Task.Delay(300);
        InfoModalManager.Close();
    }

    private void OnOpen(object? sender, Guid e)
    {
        movie = MovieStore.GetMovie(e);
        isVisible = true;
        componentIsVisible = true;

        StateHasChanged();
    }

    public void Dispose()
    {
        InfoModalManager.OnOpen -= OnOpen;
        InfoModalManager.OnClosed -= OnClosed;

        GC.SuppressFinalize(this);
    }
}