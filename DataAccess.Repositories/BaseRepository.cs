using System;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class BaseRepository<TE>: Repository<TE, string> where TE:Base<string>
    {
        public BaseRepository(DataContext context) : base(context)
        {
        }
    }
}