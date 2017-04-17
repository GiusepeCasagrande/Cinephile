using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Cinephile.Core.Rest;
using Cinephile.Core.Rest.Dtos.ImageConfigurations;
using Cinephile.Core.Rest.Dtos.Movies;
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
            return Observable
                .CombineLatest(
                    movieApiService
                        .UserInitiated
                        .FetchUpcomingMovies(apiKey),
                    movieApiService
                        .UserInitiated
                        .FetchGenres(apiKey),
                    (movies, genres) =>
                    {
                        return movies
                                .Results
                                .Select(movieDto => MapDtoToModel(genres, movieDto));
                    });
        }

        Movie MapDtoToModel(GenresDto genres, MovieResult movieDto)
        {
            return new Movie()
            {
                Title = movieDto.Title,
                PosterPath = string
                                .Concat(BaseUrl,
                                       SmallPosterSize,
                                       movieDto.PosterPath),
                Genres = genres.Genres.Where(g => movieDto.GenreIds.Contains(g.Id)).Select(j => j.Name).ToList(),
                ReleaseDate = DateTime.Parse(movieDto.ReleaseDate, new CultureInfo("en-US"))
            };
        }
    }
}