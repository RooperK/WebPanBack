using System.Collections.Generic;
using Server.Models.Tile;

namespace Server.Models.Panorama
{
    public class PanoramaFullModel : PanoramaBaseModel
    {
        public float Ath { get; set; }
        public float Aphi { get; set; }
        public float Bth { get; set; }
        public float Bphi { get; set; }
        public int ImageHqWidth { get; set; }
        public int ImageHqHeight { get; set; }
        public int ImageLqWidth { get; set; }
        public int ImageLqHeight { get; set; }
    }
}