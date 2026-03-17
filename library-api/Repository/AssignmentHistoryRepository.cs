using WebApplication2.Data;
using WebApplication2.Models.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class AssignmentHistoryRepository(AppDbContext dbContext) : IAssignmentHistoryRepository
{
    public async Task<AssignmentHistory?> CreateHistoryAsync(AssignmentHistory history)
    {
        await dbContext.AssignmentHistories.AddAsync(history);
        return await dbContext.SaveChangesAsync() > 0 
            ? history 
            : null;
    }//yeni atama geçmişi oluşturur ve veritabanına kaydeder, başarılı olursa oluşturulan geçmişi döndürür, başarısız olursa null döndürür

    public async Task<AssignmentHistory?> UpdateHistoryAsync(AssignmentHistory history)
    {
        dbContext.AssignmentHistories.Update(history);
        return await dbContext.SaveChangesAsync() > 0 
            ? history 
            : null;
    }//var olan bir atama geçmişini günceller, başarılı olursa güncellenmiş geçmişi döndürür, başarısız olursa null döndürür
}