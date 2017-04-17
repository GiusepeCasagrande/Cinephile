using System;
namespace Cinephile.Core.Rest
{
    public interface IApiService
    {
        IRestApiClient Speculative { get; }
        IRestApiClient UserInitiated { get; }
        IRestApiClient Background { get; }
    }
}
