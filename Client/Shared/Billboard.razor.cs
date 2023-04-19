using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Shared;

public partial class Billboard
{
    [Inject]
    public MovieStore.IMovies Movies { get;set; } = null!;
    [Inject]
    public IUserData UserData { get;set; } = null!;
    [Inject]
    public IInfoModalManager ModalManager { get;set; } = null!;

    private MovieStore.Movie? _movie;
    private MovieStore.Movie Movie => _movie ??= GetRandomMovie();
    private static readonly Random _random = new();

    private void OnOpen()
    {
        if(_movie != null)
            ModalManager.Open(_movie.Id);
    }

    private MovieStore.Movie GetRandomMovie()
    {
        var movies = Movies.GetAllMovies();
        var index = _random.Next(movies.Count);
        return movies[index];
    }
}