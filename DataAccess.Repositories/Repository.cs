using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class Repository<TE, TId> : IRepository<TE, TId> where TE : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TE> Data;

        public Repository(DataContext context)
        {
            _context = context;
            Data = context.Set<TE>();
        }

        public TE Add(TE data)
        {
            return Data.Add(data).Entity;
        }

        public async Task<TE> AddAsync(TE data)
        {
            return (await Data.AddAsync(data)).Entity;
        }

        public async Task<TE> GetAsync(TId id, bool isTracked = true)
        {
            var data = await Data.FindAsync(id);
            if (!isTracked)
            {
                _context.Entry(data).State = EntityState.Detached;
            }
            return data;
        }

        public async Task<bool> Contains(TId id)
        {
            return await Data.FindAsync(id) != null;
        }

        public async Task RemoveAsync(TE entity)
        {
            Data.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TE Update(TE entity)
        {
            return Data.Update(entity).Entity;
        }

        public async Task<IEnumerable<TE>> GetAllAsync()
        {
            return await Data.ToArrayAsync();
        }

        public IQueryable<TE> GetAsQueryable()
        {
            return Data.AsQueryable();
        }
        
        
    }
}