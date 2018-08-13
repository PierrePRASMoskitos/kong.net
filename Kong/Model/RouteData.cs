using Newtonsoft.Json;

namespace Kong.Model
{
    public class RouteData
    {
        public string Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public string[] Protocols { get; set; }
        public string[] Methods { get; set; }
        public string[] Hosts { get; set; }
        public string[] Paths { get; set; }
        public int RegexPriority { get; set; }
        public bool StripPath { get; set; }
        public bool PreserveHost { get; set; }
        [JsonProperty("service")]
        public ServiceInformation ServiceInformation { get; set; }
    }
}