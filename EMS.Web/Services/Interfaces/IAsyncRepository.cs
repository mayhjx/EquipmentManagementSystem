using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
