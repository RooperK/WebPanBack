using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Repositories
{
    public class TileRepository : BaseRepository<Tile>, ITileRepository
    {
        private readonly string _connection;

        public TileRepository(DataContext context, IConfiguration configuration) : base(context)
        {
            _connection = $"{Directory.GetCurrentDirectory()}/{configuration.GetConnectionString("Files")}";
            Console.WriteLine(_connection);
            if (!Directory.Exists(_connection))
            {
                Directory.CreateDirectory(_connection);
            }
        }

        public new async Task<Tile> AddAsync(Tile data)
        {
            throw new NotImplementedException();
        }

        public async Task<Tile> AddAsync(Tile data, Stream dataStream)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(ICollection<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                await AddAsync(tile);
            }
        }
        
        public async Task AddRangeAsync(IEnumerable<Tile> tiles, IEnumerable<Stream> streams)
        {
            await Data.AddRangeAsync(tiles);
        }

        public async Task<Tile> GetTile(string path, string panoramaId)
        {
            throw new NotImplementedException();
        }

        public async Task<(Tile, Stream)> GetTileStream(string path, string panoramaId, bool isHq)
        {
            throw new NotImplementedException();
        }
    }
}