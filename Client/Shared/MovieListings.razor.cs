using blazor_tailwind_netflix.Client.Extensions;
using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Shared;

public partial class MovieListings
{
    [Inject]
    public MovieStore.IMovies Movies { get;set; } = null!;
    [Inject]
    public IUserData UserData { get;set; } = null!;

    private List<MovieStore.Movie>? _favorites;

    private IReadOnlyList<MovieStore.Movie>? FavoriteMovies
    {
        get
        {
            if(_favorites is null)
            {
                _ = Task.Run(() => SetFavoriteMovies().CatchException((_) => {} ));
            }

            return _favorites?.AsReadOnly();
        }
    }
    private static readonly Random _random = new();

    protected override void OnInitialized()
    {
        UserData.MovieAddedToFavorites += MovieAdded;
        UserData.MovieRemovedFromFavorites += MovieRemoved;
    }

    private void MovieRemoved(object? sender, MovieRemovedFromFavoritesN e)
    {
        var toRemove = _favorites?.Find(f => f.Id == e.MovieId);

        if(toRemove == null)
            return;

        _favorites!.Remove(toRemove);

        StateHasChanged();
    }

    private void MovieAdded(object? sender, MovieAddedToFavorites e)
    {
        if(_favorites is null)
            return;

        var movie = Movies.GetMovie(e.MovieId);
        if(movie == null)
            return;

        _favorites.Add(movie);

        StateHasChanged();
    }

    private async Task SetFavoriteMovies()
    {
        var movieIds = (await UserData.GetFavoriteMovies(Guid.Empty)).ToList();
        if(movieIds is null)
            return;

        _favorites = movieIds.Select(m => Movies.GetMovie(m))
                             .Where(m => m != null)
                             .Select(m => m!)
                             .ToList();

        StateHasChanged();
    }

    private MovieStore.Movie GetRandomMovie()
    {
        var movies = Movies.GetAllMovies();
        var index = _random.Next(movies.Count);
        return movies[index];
    }

    public void Dispose()
    {
        UserData.MovieAddedToFavorites -= MovieAdded;
        UserData.MovieRemovedFromFavorites -= MovieRemoved;
    }
}