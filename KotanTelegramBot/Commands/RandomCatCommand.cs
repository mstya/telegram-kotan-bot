using System;
using System.Net.Http;
using System.Threading.Tasks;
using KotanTelegramBot.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KotanTelegramBot.Commands
{
    public class RandomCatCommand : IKotanCommand
    {
        private TelegramBotClient botClient;

        public RandomCatCommand(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task Execute(Message message)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://thecatapi.com");
            HttpResponseMessage msg = await hc.GetAsync("/api/images/get");
            string json = await msg.Content.ReadAsStringAsync();
            CatModel model = JsonConvert.DeserializeObject<CatModel>(json);
            await botClient.SendTextMessageAsync(message.Chat.Id, model.message); 
        }
    }
}