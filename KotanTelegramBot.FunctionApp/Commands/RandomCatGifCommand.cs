using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KotanTelegramBot.FunctionApp.Models.XML;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class RandomCatGifCommand : IKotanCommand
    {
        private TelegramBotClient botClient;

        public RandomCatGifCommand(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task Execute(Message message)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://thecatapi.com");
            HttpResponseMessage msg = await hc.GetAsync("/api/images/get?format=xml&type=gif");
            Stream stream = await msg.Content.ReadAsStreamAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(CatResponse));
            CatResponse response = (CatResponse)serializer.Deserialize(stream);
            await botClient.SendDocumentAsync(message.Chat.Id, new FileToSend(response.Data.Images.Image.Url));
        }
    }
}
