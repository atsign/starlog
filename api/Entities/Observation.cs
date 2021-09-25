using System;

namespace StarLog.Entities
{
    public class Observation
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public CelestialObject CelestialObject { get; set; }
        public string Notes { get; set; }
    }
}