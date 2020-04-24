using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public interface IKotanCommand
    {
        Task Execute(Message message);
    }
}