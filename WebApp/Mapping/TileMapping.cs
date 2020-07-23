using AutoMapper;
using Business.Contracts;
using Server.Models;
using Server.Models.Panorama;
using Server.Models.Tile;

namespace Server.Mapping
{
    public class TileMapping : Profile
    {
        public TileMapping()
        {
            CreateMap<TileDto, TileModel>();
            //CreateMap<TileDto, TileShortModel>();
        }
    }
}