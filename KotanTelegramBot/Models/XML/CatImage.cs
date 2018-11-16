using System.Xml.Serialization;

namespace KotanTelegramBot.Models.XML
{
    [XmlRoot(ElementName = "image")]
    public class CatImage
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "source_url")]
        public string Source_url { get; set; }
    }
}
