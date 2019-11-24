using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;

namespace WebjetMoviesAPI.Helper
{
    public class Utility
    {
        private const int Start = 2;

        public static string RemoveTrailing(string id)
        {
            id = id.Substring(Start);
            return id;
        }

        public static IEnumerable<Movie> GetDistinctValues(IEnumerable<Movie> result)
        {

            result = result.GroupBy(x => x.ID)
                   .Select(y => y.First())
                   .ToList();

            return result;
        }
    }
}
