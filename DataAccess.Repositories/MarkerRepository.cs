using System;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class MarkerRepository : BaseRepository<Marker>, IMarkerRepository
    {
        public MarkerRepository(DataContext context) : base(context)
        { }

        public new Task<Marker> AddAsync(Marker data)
        {
            data.Id ??= Guid.NewGuid().ToString();
            return base.AddAsync(data);
        }
    }
}