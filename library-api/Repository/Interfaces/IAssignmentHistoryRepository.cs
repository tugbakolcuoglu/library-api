using WebApplication2.Models.Entities;

namespace WebApplication2.Repository.Interfaces;

public interface IAssignmentHistoryRepository
{
    public Task<AssignmentHistory?> CreateHistoryAsync(AssignmentHistory history);
    public Task<AssignmentHistory?> UpdateHistoryAsync(AssignmentHistory history);
}