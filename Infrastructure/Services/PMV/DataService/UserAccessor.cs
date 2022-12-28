using Applications.Interfaces;

namespace Infrastructure.Services.PMV.DataService;

public class UserAccessor : IUserAccessor
{
    public Task<string> GetUserEmployeeCode() => Task.FromResult("H22095411");
}