using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Tile;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TileController : BaseController
    {
        private readonly ITileService _tileService;
        public TileController(ITileService tileService, IMapper mapper) : base(mapper)
        {
            _tileService = tileService;
        }

        [HttpGet("getHq/id={panoramaId}/{path}")]
        public async Task<IActionResult> GetHq(string path, string panoramaId)
        {
            Console.WriteLine("HQ called!");
            var tile = await _tileService.GetTile(path, panoramaId, true);
            var model = Mapper.Map<TileModel>(tile);
            
            return base.File(tile.Stream, model.Source);
        }
        [HttpGet("getLq/id={panoramaId}/{path}")]
        public async Task<IActionResult> GetLq(string path, string panoramaId)
        {
            Console.WriteLine("8K 60 FPS");
            var tile = await _tileService.GetTile(path, panoramaId, false);
            var model = Mapper.Map<TileModel>(tile);
            var tileData = base.File(tile.Stream, model.Source);
            return tileData;
        }
    }
}