using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;

namespace WebjetMoviesAPI.Services
{
    public interface IApiService
    {
        HttpClient GetUri();
        string GetResourceUrlAllMovies(MovieProviders mp);
        string GetResourceUrlMovieById(MovieProviders mp);
    }
}
