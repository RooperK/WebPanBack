using AutoMapper;
using Business.Contracts;
using DataAccess.Models;

namespace Business.Mapping
{
    public class TileDtoMapping : Profile
    {
        public TileDtoMapping()
        {
            CreateMap<TileDto, Tile>();
            CreateMap<Tile, TileDto>();
        }
    }
}