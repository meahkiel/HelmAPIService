using System.Security.Cryptography;
using System.Text;
using Applications.Contracts;
using Applications.Contracts.Interface;
using Core.Common;
using Infrastructure.Context.Abstract;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.User;

public class AuthQuery : BaseQuery<EmployeeMaster>
{
    public string EmployeeCode { get; set; }

    public AuthQuery(SqlDataConnection connection) : base(connection)
    {
        
    }

    public override async Task<EmployeeMaster> ExecuteAsync()
    {
        
        string sql = "SELECT em.EmpCode EmpCode,el.EmpPasswd EmpPasswd,em.EmpName EmpName FROM dbo.EmployeeLogin el" + 
                    " INNER JOIN EmployeeMaster em ON em.EmpCode = el.EmpCode " +
                     "WHERE el.EmpCode = @empCode";

        var result = await this.GetQuerySingle(sql,new {empCode = EmployeeCode});
        
        return result;
    }
}

public class UserAuthService : IUserAuthService
{
    private readonly AuthQuery _query;
    
    public UserAuthService(AuthQuery query)
    {
        _query = query;
    }

    public async Task<EmployeeMaster> Login(string empCode,string password) {
        
        _query.EmployeeCode = empCode;
        var result = await _query.ExecuteAsync();
        
        return result;
    }


    


   
}