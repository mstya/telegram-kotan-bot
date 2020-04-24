using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage.Table;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Queries
{
    public class FindSubscriberQuery : IRequest<TableResult>
    {
        public Message Message { get; set; }

        public FunctionContext FunctionContext { get; set; }

        public FindSubscriberQuery(Message message, FunctionContext functionContext)
        {
            Message = message;
            FunctionContext = functionContext;
        }
    }
}