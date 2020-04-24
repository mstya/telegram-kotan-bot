using System.Threading;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Models;
using KotanTelegramBot.FunctionApp.Queries;
using MediatR;
using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailyUnsubscribeCommandHandler : IRequestHandler<DailyUnsubscribeCommand>
    {
        private readonly IMediator _mediator;

        public DailyUnsubscribeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DailyUnsubscribeCommand request, CancellationToken cancellationToken)
        {
            TableResult result = await _mediator.Send(new FindSubscriberQuery(request.Message, request.FunctionContext), cancellationToken);

            if (result != null)
            {
                var deleteOperation = TableOperation.Delete((CatSubscriber)result.Result);
                await request.FunctionContext.SubscribersCloudTable.ExecuteAsync(deleteOperation);
            }
            else
            {
                // you are not subscribed.
            }
            
            return new Unit();
        }
    }
}