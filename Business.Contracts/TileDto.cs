using System;
using System.IO;

namespace Business.Contracts
{
    public class TileDto : BaseDto<string>
    {
        public string Path { get; set; }
        public string Source { get; set; }
        public bool IsHq { get; set; }
        public PanoramaDto Panorama { get; set; }
        public bool IsExternal { get; set; }
        
        public Stream Stream { get; set; }
    }
}