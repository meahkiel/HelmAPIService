using Applications.Contracts.Interface;
using Applications.Interfaces;
using Applications.UseCases.PMV.Common;
using Infrastructure.Context.Db;
using Infrastructure.Services.PMV;
using Infrastructure.Services.PMV.DataService;
using Infrastructure.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection) {

        services.AddDbContext<IDataContext, PMVDataContext>(opt => opt.UseSqlServer(dbConnection));
        
        services.AddTransient<IUnitWork,UnitWork>();

        services.AddScoped<ICommonService,CommonService>();
        services.AddScoped<Applications.UseCases.PMV.LogSheets.Interfaces.IFuelLogService, Services.PMV.DataService.FuelService>();
        services.AddScoped<Applications.UseCases.PMV.Fuels.Interfaces.IFuelLogService, Services.PMV.DataService.FuelLogService>();

        

        services.AddScoped<IUserAccessor,UserAccessor>();
        
        return services;
    }


    public static IServiceCollection AddSqlInfractructure(this IServiceCollection services,string dbConnection) {
        
        services.AddTransient<SqlDataConnection>(opt => new SqlDataConnection(dbConnection));
        
        services.AddScoped<AuthQuery>();
        services.AddScoped<UserProfileQuery>();
        services.AddScoped<IUserAuthService,UserAuthService>();
        services.AddScoped<IUserProfileService,UserProfileService>();

        return services;
    }
}