using System;
using System.Linq;
using KotanTelegramBot.Models;
using LiteDB;

namespace KotanTelegramBot.Services
{
    public class DbService : IDbService
    {
        private LiteDatabase db;

        private LiteCollection<ChatModel> chats;

        public DbService(LiteDatabase db)
        {
            this.db = db;
            this.chats = db.GetCollection<ChatModel>("chats");
        }

        public void AddChat(long id)
        {
            this.chats.Insert(new ChatModel(id));
        }

        public ChatModel GetChat(long id)
        {
            var chat = this.chats.Find(x => x.Id == id).FirstOrDefault();
            return chat;
        }
    }
}
