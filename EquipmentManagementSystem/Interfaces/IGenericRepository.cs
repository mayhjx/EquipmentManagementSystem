using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById<TType>(TType id);
        Task<IList<T>> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
