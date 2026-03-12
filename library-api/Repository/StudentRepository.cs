using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    public async Task<List<Student>> GetAllAsync()
    {
        return await dbContext.Students.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await dbContext.Students.FindAsync(id);
    }

    public async Task<Student?> GetByIdWithHistoryAsync(Guid id)
    {
        return await dbContext.Students
            .Include(s => s.AssignmentHistories) 
            .FirstOrDefaultAsync(s => s.Id == id);
        // Öğrenciyi ve onun ödev geçmişini tek bir sorguda getirir. Eğer öğrenci bulunamazsa null döner.
    }

    public async Task AddAsync(Student student)
    {
        await dbContext.Students.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    {
        dbContext.Students.Update(student);
        await Task.CompletedTask;
        // Update işlemi için EF Core zaten takip ettiği entity'leri günceller, burada ekstra bir işlem yapmamıza gerek yok.
        // Ancak async imzasını korumak için boş bir Task döndürüyoruz.
    }

    public async Task DeleteAsync(Student student)
    {
        dbContext.Students.Remove(student);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        // Bu ID'ye sahip biri var mı yok mu kontrolü
        return await dbContext.Students.AnyAsync(s => s.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        // Yapılan tüm ekleme, silme, güncelleme işlemlerini veritabanına yansıtır (Commit)
        await dbContext.SaveChangesAsync();
    }
}