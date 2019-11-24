using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebjetMoviesAPI.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public Uri Poster { get; set; }
    }
}
