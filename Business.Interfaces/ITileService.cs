using System.Threading.Tasks;
using Business.Contracts;

namespace Business.Interfaces
{
    public interface ITileService
    {
        public Task<TileDto> GetTile(string path, string panoramaId, bool isHq);
    }
}