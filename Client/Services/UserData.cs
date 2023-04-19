namespace blazor_tailwind_netflix.Client.Services;

public interface IUserData
{
    Task AddMovieToFavorites(Guid userId, Guid movieId);
    Task RemoveMovieFromFavorites(Guid userId, Guid movieId);
    Task<IReadOnlyList<Guid>> GetFavoriteMovies(Guid userId);
    event EventHandler<MovieAddedToFavorites> MovieAddedToFavorites;
    event EventHandler<MovieRemovedFromFavoritesN> MovieRemovedFromFavorites;
}

public record MovieAddedToFavorites(Guid UserId, Guid MovieId);
public record MovieRemovedFromFavoritesN(Guid UserId, Guid MovieId);

public class UserData : IUserData
{
    private readonly Dictionary<Guid, HashSet<Guid>> _usersFavouriteMovies = new();

    public event EventHandler<MovieAddedToFavorites>? MovieAddedToFavorites;
    public event EventHandler<MovieRemovedFromFavoritesN>? MovieRemovedFromFavorites;

    public Task AddMovieToFavorites(Guid userId, Guid movieId)
    {
        HashSet<Guid> favourites = GetUserFavorites(userId);

        if(favourites.Contains(movieId))
            return Task.CompletedTask;

        favourites.Add(movieId);

        MovieAddedToFavorites?.Invoke(this, new MovieAddedToFavorites(userId, movieId));

        return Task.CompletedTask;
    }

    private HashSet<Guid> GetUserFavorites(Guid userId)
    {
        HashSet<Guid> favorites;
        if(!_usersFavouriteMovies.ContainsKey(userId))
        {
            favorites = new HashSet<Guid>();
            _usersFavouriteMovies[userId] = favorites;
        }
        else
        {
            favorites = _usersFavouriteMovies[userId];
        }

        return favorites;
    }

    public Task<IReadOnlyList<Guid>> GetFavoriteMovies(Guid userId) =>
        Task.FromResult((IReadOnlyList<Guid>)GetUserFavorites(userId).ToList());

    public Task RemoveMovieFromFavorites(Guid userId, Guid movieId)
    {
        HashSet<Guid> favourites = GetUserFavorites(userId);

        if(!favourites.Contains(movieId))
            return Task.CompletedTask;

        favourites.Remove(movieId);

        MovieRemovedFromFavorites?.Invoke(this, new MovieRemovedFromFavoritesN(userId, movieId));

        return Task.CompletedTask;
    }
}