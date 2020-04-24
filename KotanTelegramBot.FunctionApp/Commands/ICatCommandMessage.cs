using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public interface ICatCommandMessage
    {
        public Message Message { get; set; }
    }
}