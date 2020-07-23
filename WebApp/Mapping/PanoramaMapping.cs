using AutoMapper;
using Business.Contracts;
using Server.Models.Panorama;

namespace Server.Mapping
{
    public class PanoramaMapping : Profile
    {
        public PanoramaMapping()
        {
            CreateMap<PanoramaCreationModel, PanoramaDto>();
            CreateMap<PanoramaDto, PanoramaBaseModel>();
            CreateMap<PanoramaDto, PanoramaFullModel>();
        }
    }
}