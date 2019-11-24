using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebjetMoviesAPI.Models;
using WebjetMoviesAPI.Services;

namespace Webjet.Controllers
{
    [Route("api")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET api/movies
        [HttpGet]
        [Route("movies")]
        public async Task<IEnumerable<Movie>> GetMovies()
        {

            return  await _movieService.GetAllMoviesAsync();
        }

        // GET api/movie/5
        [HttpGet("movie/{id}")]
        public async Task<ModifiedMovieDetails> GetMovie(string id)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie != null)
            {
                return movie;
            }

            return null;
        }

    }
}
