using System;

namespace StarLog.Entities
{
    public class Observation : Entity
    {
        public DateTime DateTime { get; set; }
        public CelestialObject CelestialObject { get; set; }
        public string Notes { get; set; }
    }
}