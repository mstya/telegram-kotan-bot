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
            var client = new TelegramBotClient("473499861:AAFz5dGwvIvPbQhx7g29c-9ARb7KTH8JMh4");
            client.SetWebhookAsync(url + "/bot/handle").Wait();
            Console.ReadLine();
            client.SetWebhookAsync().Wait();
            Console.WriteLine("Finish");
        }
    }
}