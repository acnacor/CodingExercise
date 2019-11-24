using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;


namespace WebjetMoviesAPI.Services
{
    public class ApiService : IApiService
    {
        private readonly IOptions<MovieUri> _movieUri;

        public ApiService(IOptions<MovieUri> movieUri)
        {
            _movieUri = movieUri;
        }

        public HttpClient GetUri()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_movieUri.Value.BaseUri)

            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("x-access-token", _movieUri.Value.AccessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            return httpClient;
        }

        public string GetResourceUrlAllMovies(MovieProviders mp)
        {

            if(mp.Equals(MovieProviders.Cinemaworld))
            {
                return _movieUri.Value.CinemaworldGetMovies;
            }
            else
            {
                return _movieUri.Value.FilmworldGetMovies;
            }
        }

        public string GetResourceUrlMovieById(MovieProviders mp)
        {

            if (mp.Equals(MovieProviders.Cinemaworld))
            {
                return _movieUri.Value.CinemaworldGetMovieById;
            }
            else
            {
                return _movieUri.Value.FilmworldGetMovieById;
            }
        }
    }
}
