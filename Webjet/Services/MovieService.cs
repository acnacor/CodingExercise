using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebjetMoviesAPI.Models;

namespace WebjetMoviesAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {

            IEnumerable<Movie> combinedResult = Enumerable.Empty<Movie>();

            var tasks = new List<Task<MovieList>>();

            //gets all the movies from various providers
            foreach (MovieProviders mp in Enum.GetValues(typeof(MovieProviders)))
            {
                tasks.Add(_movieRepository.GetAllMoviesAsync(mp));
            }


            await Task.WhenAll(tasks);

            foreach (var taskDetail in tasks)
            {
                if (taskDetail.Result.Movies != null)
                {
                    combinedResult = combinedResult.Concat(taskDetail.Result.Movies);
                }
            }


            return ModifyResult(combinedResult);
        }

        public async Task<ModifiedMovieDetails> GetMovieById(string id)
        {
            // Similar with the above except

            List<MovieDetails> combinedResult = new List<MovieDetails>();

            var tasks = new List<Task<MovieDetails>>();


            //gets all the movies from various providers
            foreach (MovieProviders mp in Enum.GetValues(typeof(MovieProviders)))
            {

                var response = _movieRepository.GetMovieByIdAsync(mp, id);

                if (response.Result != null)
                {
                   tasks.Add(response);
                }
              
            }

            await Task.WhenAll(tasks);

            if(tasks.Count == 0)
            {
                return null;
            }
            else
            {

                foreach (var taskDetail in tasks)
                {
                    if (taskDetail.Result != null)
                    {
                        combinedResult.Add(taskDetail.Result);

                    }
                }

                return GetModifyMovieDetail(combinedResult.FirstOrDefault(), tasks);
            }


        }

        private IEnumerable<Movie> ModifyResult(IEnumerable<Movie> combinedResult)
        {
            // Trims id,The same movies have different id num except first 2 char (fw/cw)
            foreach (Movie m in combinedResult)
            {
                m.ID = Helper.Utility.RemoveTrailing(m.ID);
            }

            // Remove duplicates from the same list
            return Helper.Utility.GetDistinctValues(combinedResult);

        }

        // To get price -> GetMovie(MP) return MovieDetails.Price (to sort dictionary make it a list first)

        private ModifiedMovieDetails GetModifyMovieDetail(MovieDetails md, List<Task<MovieDetails>> tasks)
        {

            // this is used to prevent repeating data (basiclly a  combination of two)
            if (md == null)
            {
                return new ModifiedMovieDetails();
            }

            var uniqueMoveDictionary = new Dictionary<MovieProviders, Movie>();

            ModifiedMovieDetails moveDetail = new ModifiedMovieDetails()
            {
                Title = md.Title,
                Year = md.Year,
                Rated = md.Rated,
                Released = md.Released,
                Runtime = md.Runtime,
                Genre = md.Genre,
                Director = md.Director,
                Writer = md.Writer,
                Actors = md.Actors,
                Plot = md.Plot,
                Language = md.Language,
                Country = md.Country,
                Awards = md.Awards,
                Poster = md.Poster,
                Metascore = md.Metascore,
                Rating = md.Rated,
                Votes = md.Votes,
                Prices = GetPricesOfMovieByProviders(tasks),
            };

                return moveDetail;
            }

        private List<ProviderPrice> GetPricesOfMovieByProviders(List<Task<MovieDetails>> tasks)
        {
            List<ProviderPrice> data = new List<ProviderPrice>();

            foreach (var taskDetail in tasks)
            {

                if (taskDetail.Result != null)
                {
                    // populate data by providing the price of a given movie by different providers
                    if (taskDetail.Result.ID.Substring(0, 2).Equals("cw"))
                    {

                        data.Add(new ProviderPrice
                        {
                            ProviderName = Enum.GetName(typeof(MovieProviders), MovieProviders.Cinemaworld),
                            Price = double.Parse(taskDetail.Result.Price, CultureInfo.InvariantCulture)
                        });

                    }
                    else
                    {
                        data.Add(new ProviderPrice
                        {
                            ProviderName = Enum.GetName(typeof(MovieProviders), MovieProviders.Filmworld),
                            Price = double.Parse(taskDetail.Result.Price, CultureInfo.InvariantCulture)
                        });
                    }

                }
            }


            // to display the prices in ascending order
            return data.OrderBy(p => p.Price).ToList();

        }

    }

}
