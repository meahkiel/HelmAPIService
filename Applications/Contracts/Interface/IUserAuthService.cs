namespace Applications.Contracts.Interface
{
    public interface IUserAuthService
    {
        Task<bool> Login(string empCode,string password);
    }
}