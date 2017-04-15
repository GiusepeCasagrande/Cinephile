using System;
using System.Collections.Generic;

namespace Cinephile.Core.Domain
{
    public class MovieService : IMovieService
    {
        public MovieService()
        {
        }

        IObservable<IEnumerable<Movie>> IMovieService.GetUpcomingMovies()
        {
            throw new NotImplementedException();
        }
    }
}
