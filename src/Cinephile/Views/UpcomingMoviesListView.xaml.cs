using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cinephile.Core.Model;
using Cinephile.Utils;
using Cinephile.ViewModels;
using ReactiveUI;
using Xamarin.Forms;

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

                this.Bind(ViewModel, x => x.SelectedItem, x => x.UpcomingMoviesList.SelectedItem).DisposeWith(disposables);

                UpcomingMoviesList
                    .Events()
                    .ItemAppearing
                    .Select((e) =>
                    {
                        var cell = e.Item as UpcomingMoviesCellViewModel;
                        return ViewModel.Movies.IndexOf(cell);
                    })
                    .Do(index => Debug.WriteLine($"==> index {index} >= {ViewModel.Movies.Count - 5} = {index >= ViewModel.Movies.Count - 5}"))
                    .Where(index => index >= ViewModel.Movies.Count - 5)
                    .InvokeCommand(ViewModel.LoadMovies)
                    .DisposeWith(disposables);

                Observable.Return(0).InvokeCommand(ViewModel.LoadMovies);
            });
        }
    }
}
