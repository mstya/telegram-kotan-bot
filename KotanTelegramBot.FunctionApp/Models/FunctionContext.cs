using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp.Models
{
    public class FunctionContext
    {
        public CloudTable SubscribersCloudTable { get; set; }
    }
}