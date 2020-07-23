using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IPanoramaRepository : IRepository<Panorama, string>
    {
        public Task<Panorama> GetAsync(string id, string userId, bool isTracked = true);
    }
}