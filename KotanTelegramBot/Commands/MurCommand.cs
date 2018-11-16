using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KotanTelegramBot.Commands
{
    public class MurCommand : IKotanCommand
    {
        private TelegramBotClient botClient;

        public MurCommand(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public Task Execute(Message message)
        {
            return botClient.SendTextMessageAsync(message.Chat.Id, "Mur");
        }
    }
}