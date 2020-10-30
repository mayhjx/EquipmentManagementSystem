using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Services
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly EquipmentContext _context;

        public EfRepository(EquipmentContext context)
        {
            _context = context;
        }

        public async Task<bool> IsValidAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(r => r.Id == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var isValid = await IsValidAsync(id);
            if (!isValid)
            {
                throw new ArgumentNullException("GetById Error: Not Found");
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await IsValidAsync(entity.Id))
                {
                    throw new ArgumentNullException("Update Error: Not Found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task<IList<AuditTrailLog>> GetAuditTrailLogs(T entity)
        //{
        //    return await _context.Set<AuditTrailLog>()
        //        .AsNoTracking()
        //        .Where(log => log.EntityName == entity.GetType().Name)
        //        .OrderByDescending(log => log.DateChanged)
        //        .ToListAsync();
        //}
    }
}
