using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie_App.Application;
using Movie_App.Application.Services;
using Movie_App.Domain.Entities;
using Movie_App.Domain.Interfaces;
using Movie_App.Infrastructure;
using Movie_App.Infrastructure.Services;
using Movie_App.Persistence;
using Movie_App.Persistence.Repository;

IHost host = Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new PersistenceModule());
        })
        .Build();

IUser user = new User("test@example.com");
IUser user2 = new User("Rakin123@gmail.com");
var initialMovies = new List<IMovie>
    {
        new Movie("Inception", new List<string>{"Leonardo DiCaprio", "Joseph Gordon-Levitt"}, "Action", new DateTime(2010, 7, 16), 160000000),
        new Movie("The Matrix", new List<string>{"Keanu Reeves", "Laurence Fishburne"}, "Sci-Fi", new DateTime(1999, 3, 31), 63000000),
        new Movie("The Godfather", new List<string>{"Marlon Brando", "Al Pacino"}, "Crime", new DateTime(1972, 3, 24), 6000000),
        new Movie("The Dark Knight", new List<string>{"Christian Bale", "Heath Ledger"}, "Action", new DateTime(2008, 7, 18), 185000000),
    };

IMovieRepository movieRepository = new MovieRepository();

foreach (var movie in initialMovies)
{
    try
    {
        movieRepository.AddMovie((Movie)movie);
        Console.WriteLine($"Added movie: {movie.Title}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Failed to add movie '{movie.Title}': {ex.Message}");
    }
}

// Create an instance of IUserRepository (if not already created)
IUserRepository userRepository = new UserRepository(); // Assuming you have implemented IUserRepository

userRepository.AddUser((User)user);
userRepository.AddUser((User)user2);
// Create an instance of UserMovieService
IUserMovieService userMovieService = new UserMovieService(userRepository, movieRepository);

var AllMovies = movieRepository.GetAllMovies().ToList();
// Add favorite movies for user
try
{
    userMovieService.AddFavoriteMovie((User)user, AllMovies[0].Value);
    userMovieService.AddFavoriteMovie((User)user, AllMovies[1].Value);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}

// Add favorite movies for user2
try
{
    userMovieService.AddFavoriteMovie((User)user2, AllMovies[2].Value);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}

// Retrieve favorite movies for a user
IEnumerable<IMovie> favoriteMovies = userMovieService.GetFavoriteMovies((User)user);
Console.WriteLine($"Favorite movies for {user.Email}:");
DisplayMovieDetails(favoriteMovies);

// Retrieve favorite movies for user2
favoriteMovies = userMovieService.GetFavoriteMovies((User)user2);
Console.WriteLine($"Favorite movies for {user2.Email}:");
DisplayMovieDetails(favoriteMovies);
// Search movies for a user
string keyword = "Matrix";
Console.WriteLine($"Search results for '{keyword}':");
var searchResults = userMovieService.SearchMovies((User)user, keyword);
DisplayMovieDetails(searchResults);

static void DisplayMovieDetails(IEnumerable<IMovie> movies)
{
    foreach (var movie in movies)
    {
        Console.WriteLine($"Title: {movie.Title}");

        // Concatenate cast names with comma and space separator
        string castList = string.Join(", ", movie.Cast);

        Console.WriteLine($"The casts are: {castList}");
        Console.WriteLine($"Category: {movie.Category}");
        Console.WriteLine($"Release-Date: {movie.ReleaseDate}");
        Console.WriteLine($"Budget: {movie.Budget}");

        Console.WriteLine(); // Add a blank line between movies
    }
}
await host.RunAsync();
