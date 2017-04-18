using System;
using System.Collections.Generic;

namespace Cinephile.Core.Model
{
    public class Movie
    {
        public string PosterSmall { get; set; }
        public string PosterBig { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IList<string> Genres { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
