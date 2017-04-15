using System;
using System.Collections.Generic;

namespace Cinephile.Core.Domain
{
    public interface IMovieService
    {
        IObservable<IEnumerable<Movie>> GetUpcomingMovies();
    }
}
