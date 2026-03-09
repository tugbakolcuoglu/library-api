using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly AppDbContext _dbContext;//Veritabanı işlemleri için DbContext'i kullanacağız.
    public  OperationController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }//Constructor ile DbContext'i alıyoruz.

    [HttpPost("ReturnBookFromStudent")]
    public async Task<IActionResult> ReturnBookFromStudent(Guid bookId)
    { 
        var book = await _dbContext.Books
            .Include(b => b.Student)
            .FirstOrDefaultAsync(b => b.Id == bookId);//Kitap bilgisi ve ilişkili öğrenci bilgisiyle birlikte veritabanından çekilir.

        if (book == null || book.StudentId == null)
        {
            return BadRequest("Book is not currently assigned.");
        }//Kitap mevcut değil veya şu anda atanmış değilse hata döndür.
        
        var history = new AssignmentHistory
        {
            Id = Guid.NewGuid(),
            BookId = book.Id,
            StudentId = book.StudentId.Value
        };

        _dbContext.AssignmentHistories.Add(history);
        
        book.StudentId = null; 
        book.IsAvailable = true; 

        await _dbContext.SaveChangesAsync();
        return Ok("Kitap başarıyla geri alındı ve geçmişe kaydedildi.");
    }
}