using System.Collections;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Tour : Base<string>
    {
        public string UserGuid { get; set; }
        public ICollection<Panorama> Panoramas { get; set; }
        public ICollection<Marker> Markers { get; set; }
    }
}