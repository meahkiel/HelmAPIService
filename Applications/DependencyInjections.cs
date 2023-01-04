using System.Reflection;
using Applications.Configurations.PMV;
using Applications.UseCases.PMV.LogSheets;
using Microsoft.Extensions.DependencyInjection;

namespace Applications;

public static class DependencyInjections
{
     public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(CreateLogSheetCommand).Assembly);
        services.AddValidatorsFromAssemblyContaining<LogSheetValidator>();
        services.AddAutoMapper(typeof(PMVMapProfile));
        return services;
    }
}