using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
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
                    selector: x =>
                    {
                        return new ObservableReactiveList<UpcomingMoviesCellViewModel>(x);
                    }
                );

                Observable.Return(Unit.Default).InvokeCommand(ViewModel.LoadAllDocuments);
            });
        }
    }
}
