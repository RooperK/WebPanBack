
using Microsoft.AspNetCore.Http;

namespace Server.Models.Panorama
{
    public class PanoramaCreationModel
    {
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public int TileSizeX { get; set; }
        public int TileSizeY { get; set; }
    }
}