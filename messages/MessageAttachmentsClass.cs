using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yellowantSDK 
{
    public class MessageAttachmentsClass
    {
        public string ImageUrl, ThumbUrl, Color, Text;
        public string AuthorName, AuthorIcon, AuthorLink;
        public string Footer, FooterIcon;
        public string Pretext, Title, TitleLink;
        public int status, ts;

        public List<AttachmentFieldClass> Fields = new List<AttachmentFieldClass>();
        public List<MessageButtonClass> Buttons = new List<MessageButtonClass>();

        public MessageAttachmentsClass(string ImageUrl = "", string ThumbUrl = "", string Color = "", string Text = "",
                    string AuthorName = "", string AuthorIcon = "", string AuthorLink = "",
                    string Footer = "", string FooterIcon = "", string Pretext = "", string Title = "",
                    string TitleLink = "", int status = 0, int ts = 0)
        {
            this.ImageUrl = ImageUrl;
            this.ThumbUrl = ThumbUrl;
            this.Color = Color;
            this.Text = Text;
            this.AuthorName = AuthorName;
            this.AuthorIcon = AuthorIcon;
            this.AuthorLink = AuthorLink;
            this.Footer = Footer;
            this.FooterIcon = FooterIcon;
            this.Pretext = Pretext;
            this.Title = Title;
            this.TitleLink = TitleLink;
            this.status = status;
            this.ts = ts;
        }

        public void AttachField(AttachmentFieldClass attachmentField)
        {
            Fields.Add(attachmentField);
        }

        public void AttachButton(MessageButtonClass button)
        {
            Buttons.Add(button);
        }

    }
}
