using Applications.Contracts.Interface;
using Core.Common;
using Infrastructure.Context.Abstract;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.User;

public class UserProfileQuery : BaseQuery<EmployeeMaster>
{

    public string EmployeeCode { get; set; }

    public UserProfileQuery(SqlDataConnection connection) 
        : base(connection)
    {
        
    }
    public override async Task<EmployeeMaster> ExecuteAsync()
    {
         var sql = "SELECT EmpCode, EmpName FROM dbo.EmployeeMaster WHERE EmpCode = @empCode";
         var result = await this.GetQuerySingle(sql,new {empCode = EmployeeCode});

        return result;
    }
}

public class UserProfileService : IUserProfileService
{
    private readonly UserProfileQuery _query;
    public UserProfileService(UserProfileQuery query) 
    {
        _query = query;
    }

    public async Task<EmployeeMaster?> GetUserProfile(string empCode) {

        _query.EmployeeCode = empCode;
        return await _query.ExecuteAsync();
    }
}