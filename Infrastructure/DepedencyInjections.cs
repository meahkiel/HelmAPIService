using Applications.Interfaces;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Services;
using Infrastructure.Context.Db;
using Infrastructure.Services.PMV;
using Infrastructure.Services.PMV.DataService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection) {

        services.AddDbContext<IDataContext, PMVDataContext>(opt => opt.UseSqlServer(dbConnection));
        services.AddTransient<IUnitWork,UnitWork>();
        services.AddScoped<ICommonService,CommonService>();
        services.AddScoped<ILogSheetValidationService,LogSheetValidationService>();
        
        return services;
    }
}