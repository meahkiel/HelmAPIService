using System.Reflection;
using Applications.UseCases.PMV.LogSheets;
using Microsoft.Extensions.DependencyInjection;

namespace Applications;

public static class DependencyInjections
{
     public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(CreateLogSheetRequest).Assembly);
        services.AddValidatorsFromAssemblyContaining<LogSheetValidator>();

        return services;
    }
}