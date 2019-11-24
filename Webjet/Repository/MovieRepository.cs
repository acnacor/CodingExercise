using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebjetMoviesAPI.Helper.HttpResponse;
using WebjetMoviesAPI.Models;
using WebjetMoviesAPI.Services;

namespace WebjetMoviesAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        // needed to access api uri
        private readonly IApiService _apiService;
        private readonly HttpClient client;

        public MovieRepository(IApiService apiService)
        {
            _apiService = apiService;
            client = _apiService.GetUri();
        }

        public async Task<MovieList> GetAllMoviesAsync(MovieProviders mp)
        {
            var resource = _apiService.GetResourceUrlAllMovies(mp);

            var response = await Response.GetResponse(client, resource);

            if (response != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieList>(content);
            }
            else
            {
                // instead of returning null return an empty object
                // crashing if Task.Result == null
                return await Task.FromResult<MovieList>(new MovieList());
            }

        }

        public async Task<MovieDetails> GetMovieByIdAsync(MovieProviders mp, string id)
        {
            string mpid = GenerateMovieProviderIdResource(mp);

            string resource = String.Concat(_apiService.GetResourceUrlMovieById(mp), mpid, id);

            var response = await Response.GetResponse(client, resource);

            if (response != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieDetails>(content, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            }
            else
            {
                // instead of returning null return an empty object
                // crashing if Task.Result == null
                return null;
            }
        }

        public string GenerateMovieProviderIdResource(MovieProviders mp)
        {
            if(mp.Equals(MovieProviders.Cinemaworld))
            {
                return "cw";
            }
            else
            {
                return "fw";
            }
        }
    }
}
