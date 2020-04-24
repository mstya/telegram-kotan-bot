using System.Threading;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp.Queries
{
    public class FindSubscriberQueryHandler : IRequestHandler<FindSubscriberQuery, TableResult>
    {
        public async Task<TableResult> Handle(FindSubscriberQuery request, CancellationToken cancellationToken)
        {
            TableOperation retrieve = TableOperation.Retrieve<CatSubscriber>("subscribers", request.Message.Chat.Id.ToString());
            TableResult result = await request.FunctionContext.SubscribersCloudTable.ExecuteAsync(retrieve);
            return result;
        }
    }
}