using Moq;
using Movie_App.Application.Services;
using Movie_App.Domain.Entities;
using Movie_App.Domain.Interfaces;
using Movie_App.Infrastructure.Services;
using Movie_App.Persistence.Repository;

namespace Movie_App_Unit_Tests
{
    [TestFixture]
    public class UserMovieServiceTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMovieRepository> mockMovieRepository;
        private IUserMovieService userMovieService;

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockMovieRepository = new Mock<IMovieRepository>();
            userMovieService = new UserMovieService(mockUserRepository.Object, mockMovieRepository.Object);
        }

        [Test]
        public void SearchMovies_ReturnsMatchingMovies_WhenKeywordMatchesTitle()
        {
            // Arrange
            var user = new User("test@example.com");
            var movie1 = new Movie("The Matrix", new List<string> { "Keanu Reeves", "Laurence Fishburne" }, "Sci-Fi", new DateTime(1999, 3, 31), 63000000);
            var movie2 = new Movie("Inception", new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" }, "Action", new DateTime(2010, 7, 16), 160000000);
            var movies = new List<IMovie> { movie1, movie2 };

            user.FavoriteMovies.AddRange(movies);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns(user);

            // Act
            var result = userMovieService.SearchMovies(user, "matrix");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("The Matrix", result.First().Title);
        }

        [Test]
        public void SearchMovies_ReturnsMatchingMovies_WhenKeywordMatchesActor()
        {
            // Arrange
            var user = new User("test@example.com");
            var movie1 = new Movie("The Matrix", new List<string> { "Keanu Reeves", "Laurence Fishburne" }, "Sci-Fi", new DateTime(1999, 3, 31), 63000000);
            var movie2 = new Movie("Inception", new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" }, "Action", new DateTime(2010, 7, 16), 160000000);
            var movies = new List<IMovie> { movie1, movie2 };

            user.FavoriteMovies.AddRange(movies);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns(user);

            // Act
            var result = userMovieService.SearchMovies(user, "Keanu Reeves");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("The Matrix", result.First().Title);
        }

        [Test]
        public void SearchMovies_ReturnsMatchingMovies_WhenKeywordMatchesCategory()
        {
            // Arrange
            var user = new User("test@example.com");
            var movie1 = new Movie("The Matrix", new List<string> { "Keanu Reeves", "Laurence Fishburne" }, "Sci-Fi", new DateTime(1999, 3, 31), 63000000);
            var movie2 = new Movie("Inception", new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" }, "Action", new DateTime(2010, 7, 16), 160000000);
            var movies = new List<IMovie> { movie1, movie2 };

            user.FavoriteMovies.AddRange(movies);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns(user);

            // Act
            var result = userMovieService.SearchMovies(user, "Action");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Inception", result.First().Title);
        }

        [Test]
        public void AddFavoriteMovie_UserExistsAndMovieExists_ShouldAddFavoriteMovie()
        {
            // Arrange
            var user = new User("test@example.com");
            var movie = new Movie("Inception", new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" }, "Action", new DateTime(2010, 7, 16), 160000000);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns(user);
            mockMovieRepository.Setup(repo => repo.GetMovieByTitle(movie.Title)).Returns(movie);

            // Act
            userMovieService.AddFavoriteMovie(user, movie);

            // Assert
            Assert.Contains(movie, user.FavoriteMovies);
        }

        [Test]
        public void AddFavoriteMovie_UserDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var user = new User("nonexistent@example.com");
            var movie = new Movie("Inception", new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" }, "Action", new DateTime(2010, 7, 16), 160000000);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns((User)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => userMovieService.AddFavoriteMovie(user, movie));
        }

        [Test]
        public void AddFavoriteMovie_MovieDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var user = new User("test@example.com");
            var movie = new Movie("Nonexistent Movie", new List<string>(), "Unknown", DateTime.Now, 0);
            mockUserRepository.Setup(repo => repo.GetUserByEmail(user.Email)).Returns(user);
            mockMovieRepository.Setup(repo => repo.GetMovieByTitle(movie.Title)).Returns((Movie)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => userMovieService.AddFavoriteMovie(user, movie));
        }
    }

}
