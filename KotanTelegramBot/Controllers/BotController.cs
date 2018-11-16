using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KotanTelegramBot.Commands;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KotanTelegramBot.Controllers
{
    public class BotController : Controller
    {
        private readonly Dictionary<string, IKotanCommand> commands;

        public BotController(TelegramBotClient botClient)
        {
            commands = new Dictionary<string, IKotanCommand>
            {
                { "/get_cat", new RandomCatCommand(botClient) },
                { "/get_gif_cat", new RandomCatGifCommand(botClient) },
                { "/get_cat@phenix117bot", new RandomCatCommand(botClient) },
                { "/get_gif_cat@phenix117bot", new RandomCatGifCommand(botClient) },
                { "/mur@phenix117bot", new MurCommand(botClient) },
                { "/mur", new MurCommand(botClient) }
            };
        }

        [HttpPost]
        public Task Handle([FromBody]Update update)
        {
            if (update == null)
            {
                return Task.CompletedTask;
            }

            Message message = update.Message;
            if (message?.Type == MessageType.TextMessage)
            {
                if(this.commands.TryGetValue(message.Text, out IKotanCommand command))
                {
                    return command.Execute(message);
                }
            }

            return Task.FromException(new Exception($"Command not found. Command: {message.Text}, Type: {message?.Type}"));
        }
    }
}