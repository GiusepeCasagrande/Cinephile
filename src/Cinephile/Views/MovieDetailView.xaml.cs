using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Cinephile.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Cinephile.Views
{
    public partial class MovieDetailView : ContentPageBase<MovieDetailViewModel>
    {
        public MovieDetailView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.PosterPath, x => x.Poster.Source, x => x).DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.Genres, x => x.Genres.Text, x => x).DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.ReleaseDate, x => x.ReleaseDate.Text, x => x).DisposeWith(disposables);
            });
        }
    }
}
