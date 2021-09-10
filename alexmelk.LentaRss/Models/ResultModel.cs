using Newtonsoft.Json;

namespace alexmelk.LentaRss.Models
{
    public class ResultModel
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("informationText")]
        public string InformationText { get; set; } = "";

        [JsonProperty("showInformation")]
        public bool ShowInformation { get; set; }

    }
}
