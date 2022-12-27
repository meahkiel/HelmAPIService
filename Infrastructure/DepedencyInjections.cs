using Infrastructure.Context.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection) {

        services.AddDbContext<PMVDataContext>(opt => opt.UseSqlServer(dbConnection));
        
        return services;
    }
}