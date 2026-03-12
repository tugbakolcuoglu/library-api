using WebApplication2.DTOs;

namespace WebApplication2.Services.Interfaces;

/// <summary>
///  Ogrenci Login Logout, yeni kayit ve guncelleme islemlerini yonetecek
/// </summary>
public interface IAuthService
{
    Task<bool> LoginAsync(LoginRequestDto loginDto);
}