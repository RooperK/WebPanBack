using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface ITileRepository : IRepository<Tile, string>
    {
        public Task<Tile> AddAsync(Tile data, Stream dataStream);

        public Task AddRangeAsync(ICollection<Tile> tiles);

        public Task AddRangeAsync(IEnumerable<Tile> tiles, IEnumerable<Stream> streams);

        public Task<Tile> GetTile(string path, string panoramaId);
        
        public Task<(Tile, Stream)> GetTileStream(string path, string panoramaId, bool isHq);
    }
}