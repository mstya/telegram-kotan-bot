using System.Xml.Serialization;

namespace KotanTelegramBot.FunctionApp.Models.XML
{
    [XmlRoot(ElementName = "response")]
    public class CatResponse
    {
        [XmlElement(ElementName = "data")]
        public CatData Data { get; set; }
    }
}