using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<bool> IsValidAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IList<T>> ListAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //Task<IList<AuditTrailLog>> GetAuditTrailLogs(T entity);
    }
}
