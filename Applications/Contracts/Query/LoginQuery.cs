using Applications.Contracts.Interface;

namespace Applications.Contracts.Query;

public record LoginQuery(string EmpCode,string Password) : IRequest<Result<AuthResult>>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthResult>>
{
    private readonly IUserAuthService _service;
    private readonly IUserToken _userToken;

    public LoginQueryHandler(IUserAuthService service,IUserToken userToken)
    {
        _service = service;
        _userToken = userToken;
    }


    public async Task<Result<AuthResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        try {
            
            var result = await _service.Login(request.EmpCode,request.Password);
             if(result == null) 
                throw new Exception("Invalid Username and Password");
        
            string enc = _userToken.EncodePassword(request.Password);
            if (enc != result.EmpPasswd) {
                throw new Exception("Invalid Username and Password");
            }

            var authResult = 
                new AuthResult {
                    EmpCode = result.EmpCode,
                    EmpName = result.EmpName,
                    Token = _userToken.GenerateToken(result.EmpCode,result.EmpName)
                };

            return Result.Ok(authResult);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }

  
}
