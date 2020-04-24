
using MediatR;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class SendRandomCatCommand : IRequest, ICatCommandMessage
    {
        public Message Message { get; set; }
    }
}