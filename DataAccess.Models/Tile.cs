namespace DataAccess.Models
{
    public class Tile : Base<string>
    {
        public string Path { get; set; }
        public string Source { get; set; }
        public bool IsHq { get; set; }
        public Panorama Panorama { get; set; }
        public bool IsExternal { get; set; }
    }
}