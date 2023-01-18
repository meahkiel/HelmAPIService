using System.Reflection;
using Applications.Configurations.PMV;
using Applications.UseCases.Common;
using Applications.UseCases.PMV.Fuels.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Applications;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(CreateLogCommand).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateLogValidator>();
        services.AddAutoMapper(typeof(PMVMapProfile));
        services.AddScoped<INotificationPublisher,NotificationPublisher>();

        return services;
    }

    public static IServiceCollection AddExportApplication(this IServiceCollection services) {

        
        return services;
    }
}