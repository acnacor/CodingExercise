using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;

namespace WebjetMoviesAPI.Services
{
    public interface IMovieRepository
    {
        Task<MovieList> GetAllMoviesAsync(MovieProviders mp);
        Task<MovieDetails> GetMovieByIdAsync(MovieProviders mp, string id);
    }
}
