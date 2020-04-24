using System;
using KotanTelegramBot.FunctionApp;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(Startup))]

namespace KotanTelegramBot.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient("catsApiClient", client =>
            {
                client.BaseAddress = new Uri("http://thecatapi.com");
            });
            builder.Services.AddMediatR(typeof(Startup));
            builder.Services.AddSingleton<TelegramBotClient>(x =>
            {
                var configuration = x.GetService<IConfiguration>();
                return new TelegramBotClient(configuration["TelegramSecretToken"]);
            });
        }
    }
}