using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KotanTelegramBot.Commands;
using KotanTelegramBot.Models;
using KotanTelegramBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KotanTelegramBot.Controllers
{
    public class BotController : Controller
    {
        private readonly IDbService service;

        private readonly Dictionary<string, IKotanCommand> commands;

        public BotController(IDbService service, TelegramBotClient botClient)
        {
            this.service = service;
            commands = new Dictionary<string, IKotanCommand>
            {
                { "/get_cat", new RandomCatCommand(botClient) }
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

            ChatModel chat = service.GetChat(message.Chat.Id);
            if(chat == null)
            {
                service.AddChat(message.Chat.Id);;
            }

            if (message?.Type == MessageType.TextMessage)
            {
                IKotanCommand command = this.commands[message.Text];
                return command.Execute(message);
            }

            return Task.FromException(new Exception("Command not found"));
        }
    }
}