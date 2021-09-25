using System;

namespace StarLog.Models
{
    public class ObservationModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public CelestialObjectModel CelestialObject { get; set; }
        public string Notes { get; set; }
    }
}