using System;
namespace KotanTelegramBot.Models
{
    public class ChatModel
    {
        public ChatModel(long id)
        {
            this.Id = id;
        }

        public long Id
        {
            get;
            set;
        }
    }
}
