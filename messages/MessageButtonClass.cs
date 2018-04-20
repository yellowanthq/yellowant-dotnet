using Newtonsoft.Json;

namespace yellowantSDK
{
    /*
     * ButtonClass are actionable buttons dispalyed in users Console/Messagin App.
     * Note that 'Command' property is of 'dynamic' Type. This is because structure of 'Command' can change
     * with each new 'Functions/Commands'
     */ 

    public class MessageButtonClass
    {
        [JsonProperty(PropertyName = "value")]
        public string Value;

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "command")]
        public dynamic Command = null;

        public MessageButtonClass(dynamic Command = null, string Value = "", string Name = "")
        {

            this.Value = Value;
            this.Name = Name;
            this.Command = Command;
            
        }

    }

    
}
