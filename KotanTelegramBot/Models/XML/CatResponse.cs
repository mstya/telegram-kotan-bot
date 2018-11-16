using System.Xml.Serialization;

namespace KotanTelegramBot.Models.XML
{
    [XmlRoot(ElementName = "response")]
    public class CatResponse
    {
        [XmlElement(ElementName = "data")]
        public CatData Data { get; set; }
    }
}