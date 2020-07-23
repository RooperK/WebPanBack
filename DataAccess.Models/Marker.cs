namespace DataAccess.Models
{
    public class Marker : Base<string>
    {
        // d* -  default, h* - hover, e* - expanded, l* - expandedHover
        public string Path { get; set; }
        
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        
        public Panorama Current { get; set; }
        public Panorama Next { get; set; }
        
        public bool IsExternal { get; set; }
    }
}