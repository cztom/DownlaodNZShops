using Newtonsoft.Json;

namespace DownloadNZShops {
    [JsonObject(MemberSerialization.OptIn)]
    public class NovelConfig {
        [JsonProperty]
        public string Url { get; set; }
        [JsonProperty]
        public string LastChapterNo { get; set; }
        [JsonProperty]
        public string ListXPath { get; set; }
        [JsonProperty]
        public string ContentXPath { get; set; }
    }
}