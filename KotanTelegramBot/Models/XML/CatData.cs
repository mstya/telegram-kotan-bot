using System.Xml.Serialization;

namespace KotanTelegramBot.Models.XML
{
    [XmlRoot(ElementName = "data")]
    public class CatData
    {
        [XmlElement(ElementName = "images")]
        public CatImages Images { get; set; }
    }

}
