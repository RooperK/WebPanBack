using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        public TourRepository(DataContext context) : base(context)
        {}
    }
}