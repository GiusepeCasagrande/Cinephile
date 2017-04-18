using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cinephile.Core.Model;
using Cinephile.Utils;
using Cinephile.ViewModels;
using ReactiveUI;

namespace Cinephile.Views
{
    public partial class UpcomingMoviesListView : ContentPageBase<UpcomingMoviesListViewModel>
    {
        public UpcomingMoviesListView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {

                ViewModel.SelectedItem = null;

                this.OneWayBind(ViewModel, x => x.Movies, x => x.UpcomingMoviesList.ItemsSource,
                    x =>
                    {
                        return new ObservableReactiveList<UpcomingMoviesCellViewModel>(x);
                    }
                )
                .DisposeWith(disposables);

                //this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.UpcomingMoviesList.IsRefreshing).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelectedItem, x => x.UpcomingMoviesList.SelectedItem).DisposeWith(disposables);

                Observable
                    .FromEventPattern<Xamarin.Forms.ItemVisibilityEventArgs>(x => UpcomingMoviesList.ItemAppearing += x, x => UpcomingMoviesList.ItemAppearing -= x)
                    .Select((e) =>
                    {
                        var cell = e.EventArgs.Item as UpcomingMoviesCellViewModel;
                        return ViewModel.Movies.IndexOf(cell);
                    })
                    .Where(index => index % MovieService.PageSize == 8)
                    .InvokeCommand(ViewModel.LoadMovies)
                    .DisposeWith(disposables);

            //this.BindCommand(ViewModel, x => x.LoadMovies, x => x.UpcomingMoviesList, nameof(UpcomingMoviesList.ItemAppearing)).DisposeWith(disposables);

            Observable.Return(0).InvokeCommand(ViewModel.LoadMovies);
        });
        }
}
}
