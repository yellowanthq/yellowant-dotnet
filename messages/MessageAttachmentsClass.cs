using System.Collections.Generic;
using Newtonsoft.Json;

namespace yellowantSDK 
{
    /*
     * MessageAttachment Class. Refer to YellowAnt Docs for more on how this class is displayed to end users
     */ 
    public class MessageAttachmentsClass
    {
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl;

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "thumb_url")]
        public string ThumbUrl { get; set; }

        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName;

        [JsonProperty(PropertyName = "author_link")]
        public string AuthorLink { get; set; }

        [JsonProperty(PropertyName = "author_icon")]
        public string AuthorIcon { get; set; }

        [JsonProperty(PropertyName = "footer")]
        public string Footer;

        [JsonProperty(PropertyName = "footer_icon")]
        public string FooterIcon { get; set; }

        [JsonProperty(PropertyName = "pretext")]
        public string Pretext;

        [JsonProperty(PropertyName = "title_link")]
        public string TitleLink { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status;

        [JsonProperty(PropertyName = "ts")]
        public int Ts { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<AttachmentFieldClass> Fields = new List<AttachmentFieldClass>();

        [JsonProperty(PropertyName = "buttons")]
        public List<MessageButtonClass> Buttons = new List<MessageButtonClass>();

        public MessageAttachmentsClass(string ImageUrl = "", string ThumbUrl = "", string Color = "", string Text = "",
                    string AuthorName = "", string AuthorIcon = "", string AuthorLink = "",
                    string Footer = "", string FooterIcon = "", string Pretext = "", string Title = "",
                    string TitleLink = "", int Status = 0, int Ts = 0)
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
            this.Status = Status;
            this.Ts = Ts;
        }

        //Add AttachmentField to this attachment
        public void AttachField(AttachmentFieldClass attachmentField)
        {
            Fields.Add(attachmentField);
        }


        //Add button to this attachment
        public void AttachButton(MessageButtonClass button)
        {
            Buttons.Add(button);
        }

    }
}
