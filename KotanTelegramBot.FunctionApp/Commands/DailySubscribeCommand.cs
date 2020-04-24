using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailySubscribeCommand : IRequest
    {
        public Message Message { get; set; }

        public FunctionContext FunctionContext { get; set; }
    }
}