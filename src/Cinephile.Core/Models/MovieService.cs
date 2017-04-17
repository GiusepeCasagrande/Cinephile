using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Cinephile.Core.Models;
using Cinephile.Core.Rest;
using Cinephile.Core.Rest.Dtos.ImageConfigurations;
using Splat;

namespace Cinephile.Core.Model
{
    public class MovieService : IMovieService
    {
        const string BaseUrl = "http://image.tmdb.org/t/p/";
        const string SmallPosterSize = "w185";

        private const string apiKey = "1f54bd990f1cdfb230adb312546d765d";
        private IApiService movieApiService;
        private ICache movieCache;

        public MovieService(IApiService apiService = null, ICache cache = null)
        {
            movieApiService = apiService ?? Locator.Current.GetService<IApiService>();
            movieCache = cache ?? Locator.Current.GetService<ICache>();
        }

        public IObservable<IEnumerable<Movie>> GetUpcomingMovies()
        {
            return
                movieCache
                    .GetAndFetchLatest("upcoming_movies", FetchUpcomingMovies);
        }

        IObservable<IEnumerable<Movie>> FetchUpcomingMovies()
        {
            return
                movieApiService
                    .UserInitiated
                    .FetchUpcomingMovies(apiKey)
                    .Select(dto => dto.Results.Select(movieDto => new Movie()
                    {
                        Title = movieDto.Title,
                        PosterPath = string.Concat(BaseUrl,
                                                    SmallPosterSize,
                                                   movieDto.PosterPath)
                    }));
        }

        //IObservable<IEnumerable<Gender>> FetchGenders()
        //{
        //}

    }
}