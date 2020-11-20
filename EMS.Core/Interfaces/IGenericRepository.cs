using EMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMS.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
