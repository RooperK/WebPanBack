using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<TE, in TId>
    {
        public TE Add(TE data);
        public Task<TE> AddAsync(TE data);
        public Task<TE> GetAsync(TId id, bool isTracked = true);
        public Task<bool> Contains(TId id);
        public Task RemoveAsync(TE entity);
        public Task SaveChangesAsync();
        public TE Update(TE entity);
        public Task<IEnumerable<TE>> GetAllAsync();
        public IQueryable<TE> GetAsQueryable();
    }
}