using blazor_tailwind_netflix.Client.Extensions;
using blazor_tailwind_netflix.Client.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_netflix.Client.Shared;

public partial class FavoriteButton : IDisposable
{
    [Inject]
    public IUserData UserData { get;set; } = null!;
    [Parameter]
    public Guid? MovieId { get;set; }
    [Parameter]
    public Guid? UserId { get;set; }

    private List<Guid>? _favorites;

    private List<Guid>? Favorites
    {
        get
        {
            if(_favorites is null)
            {
                _ = Task.Run(() => SetFavoriteMovies().CatchException((_) => {} ));
            }

            return _favorites;
        }
    }

    protected override void OnInitialized()
    {
        UserData.MovieAddedToFavorites += MovieAdded;
        UserData.MovieRemovedFromFavorites += MovieRemoved;
    }

    private void MovieRemoved(object? sender, MovieRemovedFromFavoritesN e)
    {
        var toRemove = _favorites?.Find(f => f == e.MovieId);

        if(!toRemove.HasValue)
            return;

        _favorites!.Remove(toRemove.Value);

        StateHasChanged();
    }

    private void MovieAdded(object? sender, MovieAddedToFavorites e)
    {
        if(_favorites is null)
            return;

        _favorites.Add(e.MovieId);

        StateHasChanged();
    }

    private bool IsFavorite => MovieId is not null && Favorites?.Contains(MovieId.Value) == true;

    private async Task AddOrRemoveFromFavorites()
    {
        if(UserId is null || MovieId is null)
            return;

        if(IsFavorite)
        {
            await UserData.RemoveMovieFromFavorites(UserId.Value, MovieId.Value);
        }
        else
        {
            await UserData.AddMovieToFavorites(UserId.Value, MovieId.Value);
        }
    }

    private async Task SetFavoriteMovies()
    {
        if(UserId is null)
            return;

        _favorites = (await UserData.GetFavoriteMovies(UserId.Value)).ToList();

        StateHasChanged();
    }

    public void Dispose()
    {
        UserData.MovieAddedToFavorites -= MovieAdded;
        UserData.MovieRemovedFromFavorites -= MovieRemoved;
    }
}
