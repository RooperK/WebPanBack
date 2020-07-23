using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Interfaces
{
    public interface IContextConfiguration<TE> : IEntityTypeConfiguration<TE> where TE: class
    {
        public IEnumerable<IConfigurationSection> Configuration { get; }
    }
}