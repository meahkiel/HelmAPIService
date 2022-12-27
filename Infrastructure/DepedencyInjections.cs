using Applications.Interfaces;
using Infrastructure.Context.Db;
using Infrastructure.Services.PMV;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection) {

        services.AddDbContext<IDataContext, PMVDataContext>(opt => opt.UseSqlServer(dbConnection));
        services.AddTransient<IUnitWork,UnitWork>();
        
        return services;
    }
}