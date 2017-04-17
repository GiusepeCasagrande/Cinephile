using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cinephile.Core.Model;
using ReactiveUI;

namespace Cinephile.ViewModels
{
    public class UpcomingMoviesListViewModel : ViewModelBase
    {
        public ReactiveList<UpcomingMoviesCellViewModel> Movies
        {
            get;
        }

        UpcomingMoviesCellViewModel m_selectedItem;
        public UpcomingMoviesCellViewModel SelectedItem
        {
            get { return m_selectedItem; }
            set { this.RaiseAndSetIfChanged(ref m_selectedItem, value); }
        }

        public ReactiveCommand<Unit, IEnumerable<Movie>> LoadMovies
        {
        	get;
        }

        ObservableAsPropertyHelper<bool> m_isRefreshing;
        public bool IsRefreshing => m_isRefreshing.Value;


        private MovieService movieService;
        IScheduler mainThreadScheduler;
        IScheduler taskPoolScheduler;

        public UpcomingMoviesListViewModel(IScheduler mainThreadScheduler = null, IScheduler taskPoolScheduler = null, IScreen hostScreen = null) : base(hostScreen)
        {
            this.mainThreadScheduler = mainThreadScheduler ?? RxApp.MainThreadScheduler;
            this.taskPoolScheduler = taskPoolScheduler ?? RxApp.TaskpoolScheduler;

            UrlPathSegment = "Upcoming Movies";

            Movies = new ReactiveList<UpcomingMoviesCellViewModel>();
            movieService = new MovieService();

            LoadMovies = ReactiveCommand
                .CreateFromObservable(
                    movieService.GetUpcomingMovies,
                    outputScheduler: this.mainThreadScheduler);

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                LoadMovies
                    .Where(movies => movies != null)
                    .Select(movies => movies.Select(movie => new UpcomingMoviesCellViewModel(movie)))
                    .Subscribe(movieViewModel =>
                    {
                        Movies.Clear();
                        Movies.AddRange(movieViewModel);
                    })
                    .DisposeWith(disposables);

                this
                    .WhenAnyValue(x => x.SelectedItem)
                    .Where(x => x != null)
                    .Subscribe(x => LoadSelectedPage(x))
                    .DisposeWith(disposables);

                LoadMovies
                    .ThrownExceptions
                    .Subscribe((obj) =>
                    {
                        Debug.WriteLine(obj.Message);
                    });

                 m_isRefreshing =
                    LoadMovies
                        .IsExecuting
                        .Select(x => x)
                        .ToProperty(this, x => x.IsRefreshing, true)
                        .DisposeWith(disposables);
            });
        }

        void LoadSelectedPage(UpcomingMoviesCellViewModel viewModel)
        {
            HostScreen.Router.Navigate.Execute(new MovieDetailViewModel(viewModel));
        }
    }
}
