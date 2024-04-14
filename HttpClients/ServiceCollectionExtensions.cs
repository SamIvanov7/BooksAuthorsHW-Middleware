using Microsoft.Extensions.DependencyInjection;
using Pur.Query.HttpClients;

namespace HttpClients;

public static class ServiceCollectionExtensions
{
    private const string DefaultHttpClientName = "Library.HttpClient";

    public static void RegisterAuthorsHttpClient(this IServiceCollection services, SystemHttpClientData systemHttpClientData)
    {
        services.RegisterHttpClient<IAuthorsHttpClient, AuthorsHttpClient>(
            systemHttpClientData,
            systemHttpClientData.HttpClientName ?? DefaultHttpClientName,
            httpClient => new AuthorsHttpClient(httpClient));
    }

    public static void RegisterBocksHttpClient(this IServiceCollection services, SystemHttpClientData systemHttpClientData)
    {
        services.RegisterHttpClient<IBocksHttpClient, BocksHttpClient>(
            systemHttpClientData,
            systemHttpClientData.HttpClientName ?? DefaultHttpClientName,
            httpClient => new BocksHttpClient(httpClient));
    }

    public static void RegisterHttpClient<TInterface, TImplementation>(
        this IServiceCollection services,
        SystemHttpClientData systemHttpClientData,
        string name,
        Func<HttpClient, TImplementation> func)
        where TInterface : class
        where TImplementation : TInterface
    {
        HttpClientRegistrator.RegisterHttpClient<TInterface, TImplementation>(services, systemHttpClientData, name, func);
    }
}
