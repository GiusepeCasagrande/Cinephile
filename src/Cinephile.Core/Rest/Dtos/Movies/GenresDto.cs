using System;
using System.Collections.Generic;

namespace Cinephile.Core.Rest.Dtos.Movies
{
    public class GenresDto
    {
        public IList<GenreDto> Genres { get; set; }

        public GenresDto()
        {
        }
    }
}
