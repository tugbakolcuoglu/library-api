using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class AuthRepository(AppDbContext dbContext) : IAuthRepository
{
    public async Task<bool> Login(string username, string password)
    {
        var user = await dbContext.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
        return user != null;
    }
}