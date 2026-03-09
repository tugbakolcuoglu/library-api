using WebApplication2.DTOs;
using WebApplication2.Entities;

namespace WebApplication2.Services.Interfaces;

/// <summary>
///  Ogrenci Login Logout, yeni kayit ve guncelleme islemlerini yonetecek
/// </summary>
public interface IAuthService
{
  
    Task<bool> LoginAsync(LoginDto loginDto);

}