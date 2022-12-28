namespace Applications.Interfaces;

public interface IUserAccessor
{
    Task<string> GetUserEmployeeCode();
}