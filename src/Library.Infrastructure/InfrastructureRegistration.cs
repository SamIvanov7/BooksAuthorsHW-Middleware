using Library.Core.Common;
using Library.Core.Domain.Authors.Common;
using Library.Infrastructure.Core.Common;
using Library.Infrastructure.Core.Domain.Authors.Common;
using Library.Infrastructure.Processing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HttpClients;
using Library.Core.Domain.Bocks.Common;
using Library.Infrastructure.Common;
using Library.Infrastructure.Core.Domain.Bocks.Common;
using Library.Infrastructure.Exceptions;
using PagesResponses;
using WebApi.Exceptions;

namespace Library.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // checkers
        services.AddScoped<IAuthorMustExistChecker, AuthorMustExistChecker>();
        services.AddScoped<IBockMustExistChecker, BockMustExistChecker>();

        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<IBocksRepository, BockRepository>();
        services.AddScoped<IBocksAuthorsRepository, BocksAuthorsRepository>();

        // exceptions
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionToResponseDeveloperMapper, ExceptionToResponseDeveloperMapper>();
        services.AddTransient<ExceptionHandlerDeveloperMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();

        // consume http clients
        services.Configure<SystemHttpClientsSettings>(configuration.GetSection(nameof(SystemHttpClientsSettings)));
        var systemHttpClientsSettings = configuration.GetSection(nameof(SystemHttpClientsSettings)).Get<SystemHttpClientsSettings>()
        ?? throw new AggregateException($"Settings: '{nameof(SystemHttpClientsSettings)}' is not found in configurations.");

        services.RegisterBocksHttpClient(systemHttpClientsSettings.LibratyManagement);
        services.RegisterAuthorsHttpClient(systemHttpClientsSettings.LibratyManagement);

        // processing
        services.AddScoped<IEnumerationIgnorer, EnumerationIgnorer>();
    }
}
