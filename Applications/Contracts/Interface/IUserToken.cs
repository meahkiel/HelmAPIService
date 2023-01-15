namespace Applications.Contracts.Interface;

public interface IUserToken
{
    string GenerateToken(string empCode,string empName);
    string EncodePassword(string password);
}