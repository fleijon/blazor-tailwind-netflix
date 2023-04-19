using System.ComponentModel;
using blazor_tailwind_netflix.Client.Extensions;
using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Pages;

public partial class Index
{
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get;set; } = null!;
    [Inject]
    public MovieStore.IMovies Movies { get;set; } = null!;
    [Inject]
    public IUserData UserData { get;set; } = null!;

    private MovieStore.Movie? _movie;
    private MovieStore.Movie Movie => _movie ??= GetRandomMovie();
    private static readonly Random _random = new();

    private MovieStore.Movie GetRandomMovie()
    {
        var movies = Movies.GetAllMovies();
        var index = _random.Next(movies.Count);
        return movies[index];
    }
}