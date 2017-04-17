using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
                
                this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.UpcomingMoviesList.IsRefreshing).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelectedItem, x => x.UpcomingMoviesList.SelectedItem).DisposeWith(disposables);

                Observable.Return(Unit.Default).InvokeCommand(ViewModel.LoadMovies);
            });
        }
    }
}
