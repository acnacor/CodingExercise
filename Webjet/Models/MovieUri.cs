using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebjetMoviesAPI.Models
{
    public class MovieUri
    {


        public string BaseUri { get; set; }

        public string CinemaworldGetMovies { get; set; }
        public string CinemaworldGetMovieById { get; set; }

        public string FilmworldGetMovies { get; set; }
        public string FilmworldGetMovieById { get; set; }

        public string AccessToken { get; set;  }

    }
}
