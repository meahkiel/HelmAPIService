using Core.Common;

namespace Applications.Contracts.Interface;

public interface IUserProfileService
{
    Task<EmployeeMaster?> GetUserProfile(string empCode);
}