using System.Xml.Serialization;

namespace KotanTelegramBot.FunctionApp.Models.XML
{
    [XmlRoot(ElementName = "images")]
    public class CatImages
    {
        [XmlElement(ElementName = "image")]
        public CatImage Image { get; set; }
    }
}
