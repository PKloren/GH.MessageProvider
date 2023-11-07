using Newtonsoft.Json;

namespace MessageProvider.Infra
{
    public class BusMessage
    {
        [JsonProperty(PropertyName = "aanvragerKey")]
        public Guid AanvragerKey { get; set; }

        [JsonProperty(PropertyName = "berichtType")]
        public string BerichtType { get; set; }

        [JsonProperty(PropertyName = "datumDagtekening")]
        public DateTime DatumDagtekening { get; set; }

        [JsonProperty(PropertyName = "kgbVariant")]
        public bool KgbVariant { get; set; }

        [JsonProperty(PropertyName = "reactieDatum")]
        public DateTime ReactieDatum { get; set; }

        [JsonProperty(PropertyName = "toeslagjaar")]
        public int Toeslagjaar { get; set; }

        public new string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
