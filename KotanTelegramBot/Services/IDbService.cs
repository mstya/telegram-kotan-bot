using KotanTelegramBot.Models;

namespace KotanTelegramBot.Services
{
    public interface IDbService
    {
        ChatModel GetChat(long id);

        void AddChat(long id);
    }
}
