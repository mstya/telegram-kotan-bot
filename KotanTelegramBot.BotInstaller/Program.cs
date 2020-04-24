using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace KotanTelegramBot.BotInstaller
{
    class Program
    {
        // set args with such format BotFunctionUrl={url} TelegramSecretToken={token}
        
        private static IConfiguration _configuration;
        static async Task Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    _configuration = hostingContext.Configuration;
                }).Build();

            await SetWebHook();
        }

        private static async Task SetWebHook()
        {
            var url = _configuration["BotFunctionUrl"];
            var client = new TelegramBotClient(_configuration["TelegramSecretToken"]);
            await client.SetWebhookAsync(url);
        }
    }
}