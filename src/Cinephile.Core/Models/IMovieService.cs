using System;
using System.Collections.Generic;

namespace Cinephile.Core.Model
{
    public interface IMovieService
    {
        IObservable<IEnumerable<Movie>> GetUpcomingMovies(int index);
    }
}
