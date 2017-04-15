using System;
using System.Reactive.Disposables;
using ReactiveUI;
using Splat;

namespace Cinephile.ViewModels
{
public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
	public string UrlPathSegment
	{
		get;
		protected set;
	}

	public IScreen HostScreen
	{
		get;
		protected set;
	}

	public ViewModelBase(IScreen hostScreen = null)
	{
		HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}

