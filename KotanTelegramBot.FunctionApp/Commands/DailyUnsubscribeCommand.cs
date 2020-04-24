using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailyUnsubscribeCommand : IRequest
    {
        public Message Message { get; set; }

        public FunctionContext FunctionContext { get; set; }
    }
}