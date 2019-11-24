using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;

namespace WebjetMoviesAPI.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<ModifiedMovieDetails> GetMovieById(string id);
    }
}
