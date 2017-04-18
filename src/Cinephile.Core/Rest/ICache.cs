using System;
namespace Cinephile.Core.Rest
{
    public interface ICache
    {
        void Initialize(string name);
        IObservable<TResult> GetAndFetchLatest<TResult>(string cacheKey, Func<IObservable<TResult>> fetchFunction);
        void InvalidateAll();
        void InvalidateAllObjects<T>() where T : class;
        void Invalidate(string key);
    }
}