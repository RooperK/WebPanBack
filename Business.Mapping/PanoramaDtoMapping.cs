using System;
using AutoMapper;
using Business.Contracts;
using DataAccess.Models;

namespace Business.Mapping
{
    public class PanoramaDtoMapping : Profile
    {
        public PanoramaDtoMapping()
        {
            CreateMap<PanoramaDto, Panorama>();
            CreateMap<Panorama, PanoramaDto>();
        }
    }
}