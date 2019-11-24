using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebjetMoviesAPI.Models;
using WebjetMoviesAPI.Services;
using WebjetMoviesAPI.Repository;
using System.Linq;
using System.IO;
using System.Reflection;

namespace WebJetMoviesAPI.Tests
{
    public class MovieControllerTest
    {
        private readonly IConfigurationRoot _config;
        private readonly MovieUri optionValue;
        private readonly IOptions<MovieUri>  options;
        private readonly ApiService  apiService;
        private readonly MovieRepository  movieRepository;
        private readonly MovieService  movieService;


        public MovieControllerTest()
        {
             _config = new ConfigurationBuilder()
           .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json")
            .Build();

            optionValue = _config.GetSection("Webjet").Get<MovieUri>();
            options = Options.Create<MovieUri>(optionValue);

            apiService = new ApiService(options);
            movieRepository = new MovieRepository(apiService);
            movieService = new MovieService(movieRepository);
        }

       [Fact]
       public async Task GetAllMovies_ReturnCombinedResponseFromApi()
        {

            // act
            var act = await movieService.GetAllMoviesAsync();

            // Assert
            Assert.Equal(7, act.Count());

        }

        [Fact]
        public async Task GetMovieById_WithInvalid_ReturnsNull()
        {
            string id = "000000";

            // act
            var act = await movieService.GetMovieById(id);

            // Assert
            Assert.Null(act);

        }

        [Fact]
        public async Task GetMovieById_WithValidIdPresentInBothProvider_ReturnsValidResponse()
        {
            string id = "0086190";

            // act
            var act = await movieService.GetMovieById(id);

            // Assert
            Assert.Equal(2, act.Prices.Count);

        }

        // Assumption the APIs are working

        [Fact]
        public async Task GetMovieById_WithValidIdPresentInOneProvider_ReturnsValidResponse()
        {
            string id = "2488496";

            // act
            var act = await movieService.GetMovieById(id);

            // Assert
            Assert.Single(act.Prices);

        }
    }
}
