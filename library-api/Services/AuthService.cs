using WebApplication2.DTOs;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var response = await authRepository.Login(loginDto.Username, loginDto.Password);
        return response;
    }
}