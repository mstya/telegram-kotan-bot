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

            if (result.Result != null)
            {
                var deleteOperation = TableOperation.Delete((CatSubscriber)result.Result);
                await request.FunctionContext.SubscribersCloudTable.ExecuteAsync(deleteOperation);
                await _mediator.Send(new ChatInformCommand(request.Message.Chat.Id, "Пока:("), cancellationToken);
            }
            else
            {
                await _mediator.Send(new ChatInformCommand(request.Message.Chat.Id, "Подпишись сначала!"), cancellationToken);
            }
            
            return new Unit();
        }
    }
}