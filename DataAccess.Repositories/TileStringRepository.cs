using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Repositories
{
    public class TileStringRepository : BaseRepository<Tile>, ITileRepository
    {
        private readonly string _connection;

        public TileStringRepository(DataContext context, IConfiguration configuration) : base(context)
        {
            _connection = $"{Directory.GetCurrentDirectory()}/{configuration.GetConnectionString("Files")}";
            Console.WriteLine(_connection);
            if (!Directory.Exists(_connection))
            {
                Directory.CreateDirectory(_connection);
            }
        }

        public new Tile Add(Tile data)
        {
            var image = data.Source;
            data.Source = null;
            var tile = Add(data);
            Console.WriteLine($"TileID null:{tile.Id == null}");
            var stream = File.CreateText($"{_connection}/{tile.Id}.b64t");
            stream.WriteLine(image);
            stream.Dispose();
            return tile;
        }

        public async Task<Tile> AddAsync(Tile data, Stream dataStream)
        {
            var tile = await base.AddAsync(data);
            await using (var stream = File.Create($"{_connection}/{tile.Id}.bdb"))
            {
                dataStream.Seek(0, SeekOrigin.Begin);
                await dataStream.CopyToAsync(stream);
                stream.Flush();
            }
            return tile;
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
            var tile = await Data.FirstAsync(t => t.Path == path && t.Panorama.Id == panoramaId);
            _context.Entry(tile).State = EntityState.Detached;
            var stream = File.OpenText($"{_connection}/{tile.Id}.b64t");
            tile.Source = await stream.ReadToEndAsync();
            stream.Close();
            return tile;
        }

        public async Task<(Tile, Stream)> GetTileStream(string path, string panoramaId, bool isHq)
        {
            var tile = await Data.FirstAsync(t => t.Path == path && t.Panorama.Id == panoramaId 
                                                                 && t.IsHq == isHq);
            _context.Entry(tile).State = EntityState.Detached;
            var stream = File.Open($"{_connection}/{tile.Id}.bdb", FileMode.Open);
            return (tile, stream);
        }
    }
}