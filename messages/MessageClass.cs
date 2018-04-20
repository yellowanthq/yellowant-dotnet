using System.Collections.Generic;
using Newtonsoft.Json;

namespace yellowantSDK

    /*
     * This is YellowAnt MessageClass. Messages to be sent should be in this format.Please refer to YellowAnt API/Docs
     * to know more about how these messages are displayed to end user in Messaging App
     */
{
    public class MessageClass
    {
        [JsonProperty(PropertyName = "message_text")]
        public string MessageText { get; set; }

        [JsonProperty(PropertyName = "attachments")]
        public List<MessageAttachmentsClass> Attachments = new List<MessageAttachmentsClass>();

        [JsonProperty(PropertyName = "requester_application")]
        public int RequesterApplication { get; set; }

        public MessageClass(string MessageText = "")
        {
            this.MessageText = MessageText;
            
        }

        public void Attach(MessageAttachmentsClass MessageAttachment)
        {
            this.Attachments.Add(MessageAttachment);
        }

    }
}
