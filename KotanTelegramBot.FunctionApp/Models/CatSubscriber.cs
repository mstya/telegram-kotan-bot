using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp.Models
{
    public class CatSubscriber : TableEntity
    {
        public long ChannelId { get; set; }
    }
}