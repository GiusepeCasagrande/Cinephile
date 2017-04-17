using System;
using Cinephile.Core.Rest.Dtos.ImageConfigurations;
using Cinephile.Core.Rest.Dtos.Movies;
using Refit;

namespace Cinephile.Core.Rest
{
    [Headers("Content-Type: application/json")]
    public interface IRestApiClient
    {
        [Get("/movie/upcoming?api_key={apiKey}&language={language}&page={page}")]
        IObservable<MovieDto> FetchUpcomingMovies(string apiKey, string language = "en-US", int page = 1);

        [Get("/configuration?api_key={apiKey}")]
        IObservable<ImageConfigurationDto> FetchImageConfiguration(string apiKey);
    }
}