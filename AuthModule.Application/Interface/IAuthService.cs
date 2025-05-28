namespace AuthModel.Service.Interface;

public interface IAuthService
{
    public Task<string> Login(string username, string password);
}