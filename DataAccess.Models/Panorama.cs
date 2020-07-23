using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Panorama : Base<string>
    {
        public string Name { get; set; }
        public string User { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Tile> Tiles { get; set; }
        //Geometry Full:0.5 * Math.PI, 2 * Math.PI, -0.5 * Math.PI, 0
        public float Ath { get; set; }
        public float Aphi { get; set; }
        public float Bth { get; set; }
        public float Bphi { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public int TileSizeX { get; set; }
        public int TileSizeY { get; set; }
        public int ImageHqWidth { get; set; }
        public int ImageHqHeight { get; set; }
        public int ImageLqWidth { get; set; }
        public int ImageLqHeight { get; set; }
    }
}