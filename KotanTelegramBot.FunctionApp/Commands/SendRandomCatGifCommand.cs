using MediatR;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class SendRandomCatGifCommand : IRequest, ICatCommandMessage
    {
        public Message Message { get; set; }
    }
}