using System;
namespace Cinephile.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        UpcomingMoviesCellViewModel viewModel;

        public MovieDetailViewModel(UpcomingMoviesCellViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
