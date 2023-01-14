using Applications.Contracts.Interface;

namespace Applications.Contracts.Query;

public record UserProfileQuery(string EmployeeCode) : IRequest<Result<UserResponse>>;

public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, Result<UserResponse>>
{
    private readonly IUserProfileService _service;

    public UserProfileQueryHandler(IUserProfileService service)
    {
        _service = service;
        
    }

    public async Task<Result<UserResponse>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        try {
            
            var result = await _service.GetUserProfile(request.EmployeeCode);
            
            return Result.Ok(new UserResponse { EmployeeCode = result.EmpCode, EmployeeName = result.EmpName});
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

        
    }
}
