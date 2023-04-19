namespace blazor_tailwind_netflix.Client.Services;

public static class MovieStore
{
    public interface IMovies
    {
        IReadOnlyList<Movie> GetAllMovies();
        Movie? GetMovie(Guid id);
    }

    public record Movie(Guid Id, string Title, string Description, string VideoUrl, string ThumbnailUrl, string Genre, TimeSpan Duration);

    public class Movies : IMovies
    {
        private readonly List<Movie> _movies;

        public Movies()
        {
            _movies = new List<Movie>()
            {
                new Movie(
                    Guid.Parse("4e2f07ad-2579-40b1-8904-cbf69d01f193"),
                    "Big Buck Bunny",
                    "Three rodents amuse themselves by harassing creatures of the forest. However, when they mess with a bunny, he decides to teach them a lesson.",
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
                    "https://upload.wikimedia.org/wikipedia/commons/7/70/Big.Buck.Bunny.-.Opening.Screen.png",
                    "Comedy",
                    TimeSpan.FromMinutes(10)
                    ),
                new Movie(
                    Guid.Parse("8c10d67e-6795-48db-a458-5f2d65866800"),
                    "Sintel",
                    "A lonely young woman, Sintel, helps and befriends a dragon, whom she calls Scales. But when he is kidnapped by an adult dragon, Sintel decides to embark on a dangerous quest to find her lost friend Scales.",
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4",
                    "http://uhdtv.io/wp-content/uploads/2020/10/Sintel-3.jpg",
                    "Adventure",
                    TimeSpan.FromMinutes(15)
                ),
                new Movie(
                    Guid.Parse("c1fabf80-beeb-4f82-b993-666f675ec133"),
                    "Tears of Steel",
                    "In an apocalyptic future, a group of soldiers and scientists takes refuge in Amsterdam to try to stop an army of robots that threatens the planet.",
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/TearsOfSteel.mp4",
                    "https://mango.blender.org/wp-content/uploads/2013/05/01_thom_celia_bridge.jpg",
                    "Action",
                    TimeSpan.FromMinutes(12)
                ),
                new Movie(
                    Guid.Parse("95b863a3-a111-40c3-8a09-e21388cd3448"),
                    "Elephant's Dream",
                    "Friends Proog and Emo journey inside the folds of a seemingly infinite Machine, exploring the dark and twisted complex of wires, gears, and cogs, until a moment of conflict negates all their assumptions.",
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                    "https://download.blender.org/ED/cover.jpg",
                    "Sci-Fi",
                    TimeSpan.FromMinutes(15)
                ),
            };
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return _movies.AsReadOnly();
        }

        public Movie? GetMovie(Guid id) => _movies.Find(m => m.Id == id);
    }
}
