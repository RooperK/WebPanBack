using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PanoramaRepository : BaseRepository<Panorama>, IPanoramaRepository
    {
        public PanoramaRepository(DataContext context) : base(context)
        {}

        public new async Task<Panorama> GetAsync(string id, bool isTracked = true)
        {
            var data = await Data.FirstAsync(panorama => panorama.Id == id && panorama.IsPublic);
            if (!isTracked)
            {
                _context.Entry(data).State = EntityState.Detached;
            }
            return data;
        }
        public async Task<Panorama> GetAsync(string id, string userId, bool isTracked = true)
        {
            var data = await Data.FirstAsync(panorama => panorama.Id == id 
                                                         && (panorama.User == userId || panorama.IsPublic));
            if (!isTracked)
            {
                _context.Entry(data).State = EntityState.Detached;
            }
            return data;
        }
    }
}