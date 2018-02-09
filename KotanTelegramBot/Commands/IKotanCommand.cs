using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace KotanTelegramBot.Commands
{
    public interface IKotanCommand
    {
        Task Execute(Message message);
    }
}