using System;
using System.Threading.Tasks;
using AutoMapper;
using BrunoZell.ModelBinding;
using Business.Contracts;
using Business.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models.Panorama;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PanoramaController : BaseController
    {
        private readonly IPanoramaService _panoramaService;
        public PanoramaController(IMapper mapper, IPanoramaService panoramaService) : base(mapper)
        {
            _panoramaService = panoramaService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> Create([FromForm]PanoramaTransportModel file)
        {
            var model = JsonConvert.DeserializeObject(file.PanoramaData, typeof(PanoramaCreationModel));
            var id = await _panoramaService.CreatePanoramaAsync(Mapper.Map<PanoramaDto>(model),
                file.Image.OpenReadStream(), "Kek");
            return Ok(id);
        }

        [HttpGet("get/id={panoramaId}")]
        public async Task<IActionResult> Get(string panoramaId)
        {
            var pan = Mapper.Map<PanoramaBaseModel>(await _panoramaService.GetPanoramaAsync(panoramaId));
            return Ok(pan);
        }
        
        [HttpGet("getFull/id={panoramaId}")]
        public async Task<IActionResult> GetFull(string panoramaId)
        {
            var pan = Mapper.Map<PanoramaFullModel>(await _panoramaService.GetPanoramaAsync(panoramaId));
            return Ok(pan);
        }
    }
}