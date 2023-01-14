using System.Security.Cryptography;
using System.Text;
using Applications.Contracts;
using Applications.Contracts.Interface;
using Infrastructure.Context.Abstract;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.User;

public class AuthQuery : BaseQuery<AuthResult>
{
    public string EmployeeCode { get; set; }

    public AuthQuery(SqlDataConnection connection) : base(connection)
    {
        
    }

    public override async Task<AuthResult> ExecuteAsync()
    {
        string sql = "SELECT EmpCode,EmpPasswd FROM dbo.EmployeeLogin WHERE EmpCode = @empCode";
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

    public async Task<bool> Login(string empCode,string password) {
        
        _query.EmployeeCode = empCode;
        var result = await _query.ExecuteAsync();
        
        if(result == null) 
            return false;
        
        string enc = EncodePassword(password);
        if (enc != result.EmpPasswd) {
            return false;
        }

        return true;

    }


    public string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encodedBytes.Length; i++)  
            {  
                //change it into 2 hexadecimal digits  
                //for each byte  
                builder.Append(encodedBytes[i].ToString("x2"));  
            }  

            //Convert encoded bytes back to a ‘readable’ string
            return builder.ToString();
        }


   
}