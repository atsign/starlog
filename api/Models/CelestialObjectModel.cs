using System.Collections.Generic;

namespace StarLog.Models
{
    public class CelestialObjectModel
    {
        public string Name { get; set; }
        public string RightAscension { get; set; }
        public string Declination { get; set; }
        public string Id { get; set; }
        public string[] CommonNames { get; set; }
    }
}