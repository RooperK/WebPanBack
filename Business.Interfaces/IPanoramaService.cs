using System.IO;
using System.Threading.Tasks;
using Business.Contracts;

namespace Business.Interfaces
{
    public interface IPanoramaService
    {
        public Task<string> CreatePanoramaAsync(PanoramaDto panorama, Stream imageData, string userId);
        public Task<PanoramaDto> GetPanoramaAsync(string guid);
        public Task<PanoramaDto> GetPanoramaAsync(string guid, string userId);
    }
}