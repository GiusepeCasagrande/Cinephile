using System;
using System.Collections.Generic;

namespace Cinephile.Core.Model
{
    public class Movie
    {
        public string PosterPath { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IList<string> Genres { get; set; }
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalLanguage { get; set; }
        public string Title { get; set; }
        public string BackdropPath { get; set; }
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
        public bool Video { get; set; }
        public double VoteAverage { get; set; }

        public Movie()
        {
        }
    }
}
