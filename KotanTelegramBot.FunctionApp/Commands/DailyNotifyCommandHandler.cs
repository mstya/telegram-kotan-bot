using System.Threading;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage.Table;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailyNotifyCommandHandler : IRequestHandler<DailyNotifyCommand>
    {
        private readonly IMediator _mediator;

        public DailyNotifyCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<Unit> Handle(DailyNotifyCommand request, CancellationToken cancellationToken)
        {
            foreach (CatSubscriber segment in 
                await request.FunctionContext.SubscribersCloudTable.ExecuteQuerySegmentedAsync(new TableQuery<CatSubscriber>(), null))
            {
                await _mediator.Send(new SendRandomCatGifCommand
                {
                    Message = new Message
                    {
                        Chat = new Chat
                        {
                            Id = segment.ChannelId
                        }
                    }
                }, cancellationToken);
            }
            return new Unit();
        }
    }
}