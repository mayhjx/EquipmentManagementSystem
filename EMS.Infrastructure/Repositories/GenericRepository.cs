using EMS.Core.Entities;
using EMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext context;
        private readonly DbSet<T> entities;

        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(T entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
