using System.IO;

namespace Server.Models.Tile
{
    public class TileModel : BaseModel<string>
    {
        public string Source { get; set; }
        public Stream Stream { get; set; }
    }
}