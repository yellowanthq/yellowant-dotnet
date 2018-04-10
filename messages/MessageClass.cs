using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yellowantSDK
{
    public class MessageClass
    {
        public string MessageText;
        public List<MessageAttachmentsClass> Attachments = new List<MessageAttachmentsClass>();
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
