using System;
using Telegram.Bot;

namespace BotWebhook
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://kotantelegrambot.azurewebsites.net";
            Console.WriteLine("Start");
            var client = new TelegramBotClient("secret");
            client.SetWebhookAsync(url + "/bot/handle").Wait();
            Console.ReadLine();
            client.SetWebhookAsync().Wait();
            Console.WriteLine("Finish");
        }
    }
}
