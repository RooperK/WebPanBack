using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Business.Contracts;
using Business.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using SixLabors.ImageSharp;

namespace Business.Services
{
    public class TileService : BaseService, ITileService
    {
        private readonly ITileRepository _tileRepository;
        public TileService(ITileRepository tileRepository,IMapper mapper) : base(mapper)
        {
            _tileRepository = tileRepository;
        }

        public async Task<TileDto> GetTile(string path, string panoramaId, bool isHq)
        {
            var (tile, stream) = await _tileRepository.GetTileStream(path, panoramaId, isHq);
            var tileDto = Mapper.Map<TileDto>(tile);
            tileDto.Stream = stream;
            return tileDto;
        }
    }
}