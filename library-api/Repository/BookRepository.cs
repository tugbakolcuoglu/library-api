using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public async Task<List<Book>> GetAllAsync()
    {
        return await context.Books
            .AsNoTracking()
            .Include(x => x.AssignmentHistories)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await context.Books
            .Include(x=> x.AssignmentHistories)
                .ThenInclude(x=> x.Student)
            .FirstOrDefaultAsync(x => x.Id == id);
        // Burda daha detayli bir response donecegimiz icin History tablosundan bir sonraki iliskili tablo olan Student tablosunu da dahil ediyoruz,
        // bu sayede kitap detay bilgisi cagrildiginda o kitaba ait odunc alma gecmisi de gonderilecek, ve her bir odunc alma gecmisi icin o ogrencinin bilgileri de gonderilecek
    }

    // Bu metodlara parametre olarak dogrudan DMO (Entity) gelecek. Burda Create isleminde Id servis katmani tarafindan basilmis hazir halde Entity gelmesi gerekiyor
    // repository sadece gelen entity'i db ye yazar/ siler/ gunceller
    public async Task AddAsync(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(Book book)
    {
        context.Books.Update(book);
        return context.SaveChangesAsync();
    }

    public Task DeleteAsync(Book book)
    {
        context.Books.Remove(book);
        return context.SaveChangesAsync();
    }
    
}