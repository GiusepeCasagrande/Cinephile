using System;
using Cinephile.Core.Model;
using ReactiveUI;

namespace Cinephile.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        public string Title
        {
            get
            {
                return this.movie.Title;
            }
        }

        public string PosterPath
        {
            get
            {
                return this.movie.PosterPath;
            }
        }

        public string Genres
        {
            get
            {
                return string.Join(", ", this.movie.Genres);
            }
        }

        public string ReleaseDate
        {
            get
            {
                return this.movie.ReleaseDate.ToString("D");
            }
        }

        public string Overview
        {
            get
            {
                return this.movie.Overview;
            }
        }

        private Movie movie;

        public MovieDetailViewModel(Movie movie, IScreen hostScreen = null) : base(hostScreen)
        {
            this.movie = movie;
        }
    }
}
