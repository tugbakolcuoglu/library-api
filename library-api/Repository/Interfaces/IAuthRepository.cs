namespace WebApplication2.Repository.Interfaces;

public interface IAuthRepository
{
    Task<bool> Login(string username, string password);
}