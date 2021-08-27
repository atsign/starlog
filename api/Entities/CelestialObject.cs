using Newtonsoft.Json;

namespace StarLog.Entities
{
    public class CelestialObject
    {
        public string Name { get; set; }
        public string RA { get; set; }
        public string Dec { get; set; }
        public string Id { get; set; }
        [JsonProperty("Common names")]
        public string CommonNames { get; set; }
    }
}