using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using DataAccess.Interfaces;
using Business.Contracts;
using Business.Contracts.Exceptions;
using Business.Interfaces;
using DataAccess.Models;
using Npgsql;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using Rectangle = SixLabors.ImageSharp.Rectangle;

namespace Business.Services
{
    public class PanoramaService : BaseService, IPanoramaService
    {
        private readonly IPanoramaRepository _panoramaRepository;
        private readonly ITileRepository _tileRepository;
        private readonly IUserService _userService;

        public PanoramaService(IPanoramaRepository panoramaRepository, ITileRepository tileRepository,
            IUserService userService, IMapper mapper) 
            : base(mapper)
        {
            _panoramaRepository = panoramaRepository;
            _tileRepository = tileRepository;
            _userService = userService;
        }

        private static bool IsPowerOfTwo(int n) 
        { 
            return n != 0 && (n & (n - 1)) == 0;
        }
        public static (TileDto[], Stream[]) TransformPanoramaStream(Stream imageData, int tileSizeX, int tileSizeY, 
            out (int imageWidth, int imageHeight, int imageWidthLq, int imageHeightLq) imageSize)
        {
            var image = Image.Load(imageData, out var format);
            var tileWidth = Math.Abs(tileSizeX);
            var tileHeight = Math.Abs(tileSizeY);
            if (tileWidth > image.Width || tileWidth < 1)
            {
                tileWidth = image.Width / 4;
            }
            if (tileHeight > image.Height || tileHeight < 1)
            {
                tileHeight = image.Height / 4;
            }
            if (!IsPowerOfTwo(tileWidth))
            {
                tileWidth = (int) Math.Floor(Math.Log2(tileWidth));
            }
            if (!IsPowerOfTwo(tileHeight))
            {
                tileHeight = (int) Math.Floor(Math.Log2(tileHeight));
            }
            var tileCountWidth = image.Width / tileWidth;
            if (image.Width % tileWidth != 0)
            {
                image.Mutate(action 
                    => action.Resize(tileCountWidth * tileWidth, 0));
            }
            var tileCountHeight = image.Height / tileHeight;
            (TileDto[], Stream[]) tiles;
            var cropRectangle = new Rectangle{X = 0, Width = tileWidth};
            int size;
            if (image.Height % tileHeight != 0)
            {
                size = tileCountWidth * (tileCountHeight + 1) + 1;
                tiles = (new TileDto[size], new Stream[size]);
                var height = tileCountHeight * tileHeight;
                cropRectangle.Y = height;
                cropRectangle.Height = image.Height - height;
                var offset = tileCountWidth * tileCountHeight;
                for (var x = 0; x < tileCountWidth; x++)
                {
                    tiles.Item2[offset + x] = new MemoryStream();
                    var tile = image.Clone(action => action.Crop(cropRectangle));
                    tile.Save(tiles.Item2[offset + x], format);
                    cropRectangle.X += tileWidth;
                    tiles.Item1[offset + x] = new TileDto
                    {
                        Path = $"{x}-{tileCountHeight}",
                        Source = $"{format.DefaultMimeType}",
                        IsHq = true,
                        IsExternal = false
                    };
                }
                cropRectangle.X = 0;
                cropRectangle.Y = 0;
                Console.WriteLine("Non unifrom!");
            }else
            {
                size = tileCountWidth*tileCountHeight+1;
                tiles = (new TileDto[size], new Stream[size]);
            }
            cropRectangle.Height = tileHeight;
            for (var y = 0; y < tileCountHeight; y++)
            {
                for (var x = 0; x < tileCountWidth; x++)
                {
                    var s = y * tileCountWidth + x;
                    tiles.Item2[s] = new MemoryStream();
                    var tile = image.Clone(action => action.Crop(cropRectangle));
                    tile.Save(tiles.Item2[s], format);
                    tiles.Item1[s] = new TileDto{
                        Path = $"{x}-{y}", 
                        Source = format.DefaultMimeType,
                        IsHq = true, 
                        IsExternal = false
                    };
                    cropRectangle.X += tileWidth;
                }
                cropRectangle.X = 0;
                cropRectangle.Y += tileHeight;
            }

            var lq = image.Clone(action => action.Resize(tileWidth, 0));
            size--;
            tiles.Item2[size] = new MemoryStream();
            lq.Save(tiles.Item2[size], format);
            tiles.Item1[size] = new TileDto
            {
                Id = Guid.NewGuid().ToString(),
                Path = "0-0",
                Source = format.DefaultMimeType,
                IsHq = false,
                IsExternal = false
            };
            imageSize = (imageWidth: image.Width, imageHeight: image.Height, imageWidthLq: lq.Width,
                imageHeightLq: lq.Height);
            return tiles;
        }

        public async Task<string> CreatePanoramaAsync(PanoramaDto panorama, Stream imageData, string userId)
        {
            //if(!await _userService.ContainsUserById(panorama.UserGuid)) throw new UserException();
            
            var (tileDtos, streams) = TransformPanoramaStream(imageData, panorama.TileSizeX, panorama.TileSizeY,
                out var imageSize );
            panorama.ImageHqWidth = imageSize.imageWidth;
            panorama.ImageHqHeight = imageSize.imageHeight;
            panorama.ImageLqWidth = imageSize.imageWidthLq;
            panorama.ImageLqHeight = imageSize.imageHeightLq;
            panorama.User = userId;
            var dbPanorama = Mapper.Map<Panorama>(panorama);
            await _panoramaRepository.AddAsync(dbPanorama);
            dbPanorama.Tiles = new List<Tile>();
            Console.WriteLine($"Pan:{dbPanorama.Id}");
            for (var i = 0; i < tileDtos.Length; i++)
            {
                var tileDto = tileDtos[i];
                var tile = Mapper.Map<Tile>(tileDto);
                Console.WriteLine($"Tile:{tile.Path}");
                try
                {
                    tile = await _tileRepository.AddAsync(tile, streams[i]);
                    dbPanorama.Tiles.Add(tile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new InternalException();
                }
            }

            await _panoramaRepository.SaveChangesAsync();
            foreach (var dbPanoramaTile in dbPanorama.Tiles)
            {
                Console.WriteLine(dbPanoramaTile.Id);
            }
            return dbPanorama.Id;
        }
        
        public async Task<PanoramaDto> GetPanoramaAsync(string guid)
        {
            Panorama panorama;
            try
            {
                panorama = await _panoramaRepository.GetAsync(guid, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotFoundException();
            }
            return Mapper.Map<PanoramaDto>(panorama);
        }
        public async Task<PanoramaDto> GetPanoramaAsync(string guid, string userId)
        {
            Panorama panorama;
            try
            {
                panorama = await _panoramaRepository.GetAsync(guid, userId,false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotFoundException();
            }
            return Mapper.Map<PanoramaDto>(panorama);
        }
    }
}