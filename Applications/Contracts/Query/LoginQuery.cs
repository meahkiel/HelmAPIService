using Applications.Contracts.Interface;

namespace Applications.Contracts.Query;

public record LoginQuery(string EmpCode,string Password) : IRequest<Result<Unit>>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<Unit>>
{
    private readonly IUserAuthService _service;

    public LoginQueryHandler(IUserAuthService service)
    {
        _service = service;
    }


    public async Task<Result<Unit>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        try {

            var isAuth = await _service.Login(request.EmpCode,request.Password);
            if(!isAuth)
                throw new Exception("Invalid Username or Password");
            return Result.Ok(Unit.Value);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

    }
}
