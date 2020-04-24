using System.Threading;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Models;
using KotanTelegramBot.FunctionApp.Queries;
using MediatR;
using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailySubscribeCommandHandler : IRequestHandler<DailySubscribeCommand>
    {
        private readonly IMediator _mediator;

        public DailySubscribeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<Unit> Handle(DailySubscribeCommand request, CancellationToken cancellationToken)
        {
            TableResult result = await _mediator.Send(new FindSubscriberQuery(request.Message, request.FunctionContext), cancellationToken);

            if (result.Result == null)
            {
                TableOperation insertOperation = TableOperation.Insert(new CatSubscriber
                {
                    ChannelId = request.Message.Chat.Id,
                    PartitionKey = "subscribers",
                    RowKey = request.Message.Chat.Id.ToString()
                });
                await request.FunctionContext.SubscribersCloudTable.ExecuteAsync(insertOperation);
            }
            else
            {
                // you are already subscribed.
            }
            
            return new Unit();
        }
    }
}