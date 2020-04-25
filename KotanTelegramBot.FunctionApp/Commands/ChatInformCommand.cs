using KotanTelegramBot.FunctionApp.Models;
using MediatR;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class ChatInformCommand : IRequest
    {
        public long ChatId { get; set; }

        public string Message { get; set; }

        public ChatInformCommand(long chatId, string message)
        {
            ChatId = chatId;
            Message = message;
        }
    }
}