using Newtonsoft.Json;

namespace yellowantSDK
{
    /*
     * AttachmentField Class
     */ 
    public class AttachmentFieldClass
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "short")]
        public int Short { get; set; }
    }
}
