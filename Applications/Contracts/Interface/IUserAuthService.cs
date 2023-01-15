using Core.Common;

namespace Applications.Contracts.Interface
{
    public interface IUserAuthService
    {
        Task<EmployeeMaster> Login(string empCode,string password);
    }
}