using Microsoft.AspNetCore.Http;

namespace Server.Models.Panorama
{
    public class PanoramaTransportModel
    {
        public string PanoramaData { get; set; }
        public IFormFile Image { get; set; }
    }
}