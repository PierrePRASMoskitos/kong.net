using Newtonsoft.Json;

namespace Kong.Model
{
    public class ServiceInformation
    {
        [JsonProperty("id")]
        public string ServiceId { get; set; }
    }
}